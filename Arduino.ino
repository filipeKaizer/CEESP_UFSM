////////////////////////////////   Modbus Master   ////////////////////////////////
#include <ModbusMaster.h>
ModbusMaster node;          // instantiate ModbusMaster object
#define MAX485_DE      3    // Pino de sentido de transmissão
#define MMW2_ID        1    // ID do dispositivo medidor MMW02
#define AddrNum        17   // Numero de endereços obtidos pelo MMW02
#define refresh        1000 // Tempo entre atualizações de dados do arduino (1000 -> 1seg)
#define tempoLimite    10   // Tempo máximo de espera do envio de dados pelo MW02

//---------------------------------------------------------------------------------
///////////////////////////////   Variaveis   /////////////////////////////////////
bool rele_status = false;   // Indica o estado do rele RL do medido MMW02
bool ConEst = false;        // Flag que controla o envio de dados para o computador. Se não houver um, o mesmo não envia
unsigned int tempoLido=0;

//// Especifica os endereços de holding, coil ou inputs a serem lidos.          ///
//// Obs: formato decimal do tipo short                                         ///
uint32_t enderecos[AddrNum] = {    
     2,   // Tensão média - 0
     4,   // Tensão de A    - 1
     6,   // Tensão de B    - 2
     8,   // Tensão de C    - 3
     10,  // Corrente média - 4
     12,  // Corrente de A  - 5
     14,  // Corrente de B  - 6
     16,  // Corrente de C  - 7
     26,  // FP total       - 8
     28,  // FP de A        - 9
     30,  // FP de B        - 10
     32,  // FP de C        - 11
     34,  // Car. FP total  - 12
     36,  // Car. FP de A   - 13
     38,  // Car. FP de B   - 14
     40,  // Car. FP de C   - 15  
     66,  // Freq. Inst.    - 16
  };
  
float valores[AddrNum];           // Vetor que armazenará os resultados

//---------------------------------------------------------------------------------

//////////////////////////////// SETUP ////////////////////////////////
void setup(){
  pinMode(MAX485_DE, OUTPUT);
  digitalWrite(MAX485_DE, 0);                         
  Serial.begin(9600);                           // Monitor serial para visualização de dados.
  Serial2.begin(9600, SERIAL_8N1);              // Serial para comunicação com o MMW02
  node.begin(MMW2_ID, Serial2);                 // Inicia o objeto node para comunicação com o MMW02
   
  node.preTransmission(preTransmission);
  node.postTransmission(postTransmission);
}

//-----------------------------------------------------------------------------------
//////////////////////////////// LOOP ///////////////////////////////////////////////
void loop()
{
  int req = softwareReq(); //Verifica requisição do software
  if (req == 1) {//test
    testaConexao(); 
  }
  if (req == 2) {//snd
    enviaValores();
  } else {//leitura de dados
     tempoLido = millis();
     for(int i=0; i<AddrNum; i++){
       valores[i] = Leitura(enderecos[i], 1);
     }      
  } 
  delay(refresh);
}


//////////////////////// LEITURA/ESCRITA //////////////////////////////////////////////
void Escrita(uint32_t address, uint8_t value) {
    preTransmission();
    node.writeSingleRegister(address,value);
    postTransmission();
}


float Leitura(uint32_t address, int option){
  uint16_t result, result2;
  float floatValue;
  uint8_t readStatus;
  
   if (0 == true)
   {
    return 0;
   } else {
     readStatus = node.readInputRegisters(address, 1); // Lê valores do tipo Imput     
     result = node.getResponseBuffer(0);  
     readStatus = node.readInputRegisters((address+1), 1); // Lê valores do tipo Imput     
     result2 = node.getResponseBuffer(0);   
      
     if (readStatus == node.ku8MBSuccess) {
      if (address >= 34 && address <=40) {
        int r = unificarInt(result, result2);
         return (0.0+r);
      } else {
         float r = unificar(result, result2);
         return r;
      }
     } else {
         return -1;
     }
   }
}
//--------------------------------------------------------------------------------------


//////////////////////////////// MODBUS MASTER /////////////////////////////////////////
void   preTransmission(){
  digitalWrite(MAX485_DE, 1);     // define direção de comunicação em alto (envio)
}
void   postTransmission(){
  digitalWrite(MAX485_DE, 0);     // define direção de comunicação em baixo (recepção)
}

//-------------------------------------------------------------------------------------


//////////////////////////////////// CONEXÃO //////////////////////////////////////////

// Função responsavel por testar conexão com o computador via serial.
void testaConexao(){
    if (Serial.available() > 0) {
      String dadosRecebidos = Serial.readStringUntil('\n');
  
      // Parsing dos valores (supõe que os valores são separados por vírgula)
      int posicaoVirgula = dadosRecebidos.indexOf(',');
      if (posicaoVirgula != -1) {
       int valor1 = dadosRecebidos.substring(0, posicaoVirgula).toInt();
       int valor2 = dadosRecebidos.substring(posicaoVirgula + 1).toInt();
  
       int result = (valor1 % valor2) * (valor1+valor2);
  
       Serial.println(result);
      } else {
      }
   }
}


// Função responsável por receber comandos do software
int softwareReq(){
  if (Serial.available() > 0){
    String msg = Serial.readStringUntil('\n');

    if (msg == "test"){
      return 1;  
    } else if (msg == "snd") {
      return 2; 
    } else {
      return 0;
    }
  } else {
    return 0;
  }
}

// Função responsável pela concatenação e envio de valores ao computador
void enviaValores(){
  String mensagem="";
    
  for (int i=0; i<AddrNum; i++){
    if(i!= 0 && i!=AddrNum){
      mensagem += ";";
    }
    mensagem = mensagem + (String)valores[i];
  }
  Serial.println(mensagem);
}
//-------------------------------------------------------------------------


/////////////////// FUNÇÕES RESPONSÁVEIS PELA UNIFICAÇÃO //////////////////  
int unificarInt(uint16_t a, uint16_t b){
  uint32_t combinado = ((uint32_t)a << 16) | b;
  int f;
  memcpy(&f, &combinado, sizeof f);
  return f;
}

float unificar(uint16_t a, uint16_t b){            
  uint32_t combinado = ((uint32_t)a << 16) | b;
  float f;
  memcpy(&f, &combinado, sizeof f);
  return f;
}
