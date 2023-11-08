////////////////////////////////   Modbus Master   ////////////////////////////////
#include <ModbusMaster.h>
ModbusMaster node;          // instantiate ModbusMaster object
#define MAX485_DE      3    // Pino de sentido de transmissão
#define MMW2_ID        1    // ID do dispositivo medidor MMW02
#define AddrNum        80   // Numero de endereços obtidos pelo MMW02
#define refresh        1000 // Tempo entre atualizações de dados do arduino (1000 -> 1seg)
#define tempoLimite    1000  // Tempo máximo de espera do envio de dados pelo MW02 (100 -> 0,1seg)


//---------------------------------------------------------------------------------
///////////////////////////////   Variaveis   /////////////////////////////////////
bool rele_status = false;   // Indica o estado do rele RL do medido MMW02
bool ConEst = false;        // Flag que controla o envio de dados para o computador. Se não houver um, o mesmo não envia
unsigned int tempoLido=0;

//// Especifica as portas usadas pelos reles de comando.
int RL[8] = {22,24,26,28,30,32,34,36}; //
bool RLStatus[8];

//// Especifica o formato de cada leitura
struct Valor {
  int add;
  float valor;
};

//// Especifica um vetor da struct valor
Valor valores[AddrNum];

//////////////////////////////// SETUP ////////////////////////////////
void setup(){
  pinMode(MAX485_DE, OUTPUT);
  digitalWrite(MAX485_DE, 0);                         
  Serial.begin(9600);                           // Monitor serial para visualização de dados.
  Serial2.begin(9600, SERIAL_8N1);              // Serial para comunicação com o MMW02
  node.begin(MMW2_ID, Serial2);                 // Inicia o objeto node para comunicação com o MMW02

  //Inicializa os reles:
  for(int i=0; i<8; i++){
    pinMode(RL[i],OUTPUT); 
    RLStatus[i] = false;
    digitalWrite(RL[i], LOW); 
  }
  
  node.preTransmission(preTransmission);
  node.postTransmission(postTransmission);
}
// ----------------------------------------------------------------------------------

//////////////////////////////// LOOP ///////////////////////////////////////////////
void loop()
{
  int req = softwareReq(); //Verifica requisição do software
  
  switch(req){
    case 1:
      testaConexao();
    break;
    case 2:
      enviaValores();
    break;
    case 3:
      controlaRele();
    break;
    case 4:
      mostraSerial();
    break;
    default:
     tempoLido = millis();
     Leitura();
    break;    
  }
  delay(refresh);
}
// ------------------------------------------------------------------------------------

//////////////////////// CONTROLE DE RELES ////////////////////////////////////////////
void controlaRele(){
  unsigned int Tinicio=millis(); //Tempo inicial
  while( !(Serial.available() > 0) && (millis() - Tinicio < 4000)); //Aguarda conexão de até 4s;
  if (Serial.available() > 0){
    int msg = Serial.parseInt();
    switch(msg){
      case 1:
      break;
      case 2:
      break;
      case 3:
      break;
    }
  }
}
//-------------------------------------------------------------------------------------

//////////////////////// LEITURA/ESCRITA //////////////////////////////////////////////
void Escrita(uint32_t address, uint8_t value) {
    preTransmission();
    node.writeSingleRegister(address,value);
    postTransmission();
}


float Leitura(){
  uint16_t result, result2;
  float floatValue;
  uint8_t readStatus;
  
  readStatus = node.readInputRegisters(2, AddrNum);

  if (readStatus == node.ku8MBSuccess) {
    float res = 0;
    
    for (int address = 0, pos = 0; address < AddrNum; address += 2, pos++) {
 
        // Obtem os dados em buffer
        result = node.getResponseBuffer(address);
        result2 = node.getResponseBuffer(address + 1);
        
        // É baseado em caracteristica, usa unificação inteira
        if (address >= 34 && address <= 40) {
          res = unificarInt(result, result2);
        } else {
        // É baseado em flutuante, usa unificação flutuante
          res = unificarFloat(result, result2);
        }

        valores[pos].add = address+2;
        valores[pos].valor = res;
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

//// FUNÇÃO RESPONSÁVEL POR OBTER OS COMANDOS DO COMPUTADOR
int softwareReq(){
  if (Serial.available() > 0){
    String msg = Serial.readStringUntil('\n');

    if (msg == "test"){
      return 1;  
    } else if (msg == "snd") {
      return 2; 
    } else if (msg == "rele") {
      return 3;
    } else if (msg == "serial"){
      return 4;
    } else {
      return 0;
    }
  } else {
    return 0;
  }
}

// FUNÇÃO RESPONSÁVEL PELA CONCATENAÇÃO E ENVIO DE DADOS AO OMPUTADOR //////
// Envia valores para o software
void enviaValores(){
  String mensagem="";
  String id="";
  for (int i=0; i<AddrNum/2; i++){
    if(i!= 0 && i!=AddrNum){
      mensagem += ";";
    }
    id = "";

    switch(valores[i].add) {
      case 2:
        id = "Vm";
      break;
      case 4:
        id = "Va";
      break;
      case 6:
        id = "Vb";
      break;
      case 8:
        id = "Vc";
      break;
      case 10:
        id = "Im";
      break;
      case 12:
        id = "Ia";
      break;
      case 14:
        id = "Ib";
      break;
      case 16:
        id = "Ic";
      break;
      case 26:
        id = "FPt";
      break;  
      case 28:
        id = "FPa";
      break;
      case 30:
        id = "FPb";
      break;
      case 32:
        id = "FPc";
      break;
      case 36:
        id = "CFPt";
      break;
      case 38:
        id = "CFPa";
      break;
      case 40:
        id = "CFPb";
      break;
      case 42:
        id = "CFPc";
      break;
      case 66:
        id = "F";
      break;
    }
    
    mensagem = mensagem + id + "=" + (String)valores[i].valor;
  }
  Serial.println(mensagem);
}

// Mostra valores com legenda no monitor serial
void mostraSerial() {
  for (int add = 0; add < AddrNum/2; add++) {
    switch(valores[add].add) {
      case 2:
        Serial.print("Vm: ");
      break;
      case 4:
        Serial.print("Va: ");
      break;
      case 6:
        Serial.print("Vb: ");
      break;
      case 8:
        Serial.print("Vc: ");
      break;
      case 10:
        Serial.print("Im: ");
      break;
      case 12:
        Serial.print("Ia: ");
      break;
      case 14:
        Serial.print("Ib: ");
      break;
      case 16:
        Serial.print("Ic: ");
      break;
      case 26:
        Serial.print("FPt: ");
      break;  
      case 28:
        Serial.print("FPa: ");
      break;
      case 30:
        Serial.print("FPb: ");
      break;
      case 32:
        Serial.print("FPc: ");
      break;
      case 36:
        Serial.print("CFPt: ");
      break;
      case 38:
        Serial.print("CFPa: ");
      break;
      case 40:
        Serial.print("CFPb: ");
      break;
      case 42:
        Serial.print("CFPc: ");
      break;
      case 66:
        Serial.print("F: ");
      break;
    }
    Serial.print(valores[add].valor);
    Serial.print("\n");
  }
}
//-------------------------------------------------------------------------



/////////////////// FUNÇÕES RESPONSÁVEIS PELA UNIFICAÇÃO //////////////////  
int unificarInt(uint16_t a, uint16_t b){
  uint32_t combinado = ((uint32_t)a << 16) | b;
  int f;
  memcpy(&f, &combinado, sizeof f);
  return f;
}

float unificarFloat(uint16_t a, uint16_t b){            
  uint32_t combinado = ((uint32_t)a << 16) | b;
  float f;
  memcpy(&f, &combinado, sizeof f);
  return f;
}
