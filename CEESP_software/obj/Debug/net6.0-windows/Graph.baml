   M S B A M L   `   `   `  ����I  ECEESP_software, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:CEESP_softwareCEESP_software  5          CEESP_software.Graph   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:CEESP_software     5      R NOxyPlot.Wpf, Version=2.1.2.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0Y UOxyPlot.Wpf.Shared, Version=2.1.2.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0"oxyhttp://oxyplot.org/wpf   5        N� Title$  Graph��5
       � 	Resources 5         Mostra_Check �� 
TargetName �� TargetProperty9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value  Esconde_Check   EasingFunction.  (System.Windows.Media.Animation.CubicEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode��%�  &	        &	 �    �� 5   
   .��5      U� $ 	CheckList��6,   $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�6N   .\�5        $ 00:00:00��6'   $ 0Q�6:     5      $ 00:00:00.2000000��6'   $	 116Q�6B   5      U� 5      $ 	CheckList��6,   $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)�6N   .\�5        $ 00:00:00��6'   $ 0Q�6:     5      $ 00:00:00.2000000��6'   $ 4Q�6B   5      5      �� 5   
   .��5      U� $ 	CheckList��6,   $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�6N   .\�5        $ 00:00:00��6'   $ 116.666Q�6:    5        5      $ EaseOut=�6$   5        5      $ 00:00:00.2000000��6'   $ 0Q�6B    5        5      $ EaseOut=�6$   5!      5"      U� 5#      $ 	CheckList��6,   $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)�6N   .\�5$        $ 00:00:00��6'   $ 0.667Q�6:    5%        5&      $ EaseOut=�6$   5(        5)      $ 00:00:00.2000000��6'   $ 0Q�6B    5*        5+      $ EaseOut=�6$   5-      5.      5/      50      .��52      � .+�54   
     OxyPlot.Wpf.PlotView  -   5          Name$ 	PlotGraph��54      $��Center=�6*   $��Center=�6G   $	��450��6b   $	��800��6o   ���	#330101016{   �� 55   
   -   5        	 ��Name$	 btReturn��55      $�� ��6#   $
��Left=�6.   $��
10,410,0,0q�6I   $	��Top=�6]   $��30��6u   $��30��6�   
 � Cursor$

 Hand��6�   4  .System.Windows.Automation.AutomationProperties   HelpText$' !Retornar para a página de dados.��6�   ��6�   Z� ��6�   Z�  J� OpacityMask 56      �� 57       �� ImageSource$ /icons/graph.png��6   ��59      �� 5:      $ /icons/graph.png��6   5<      �� 5=   
   -   5        $	 	btLegenda��5=      $�� ��6$   $
��Left=�6/   $��
57,410,0,0q�6J   $	��Top=�6^   $��30��6v   $��30��6�   $

 Hand��6�   $, &Seleção de valores a serem mostrados��6�   ��6�   Z� ��6�   Z�  5>      �� 5?      $ /icons/checkList.png��6   ��5A      �� 5B      $ /icons/checkList.png��6   5D      �� 5E   
   -   5         ��Name$ Check_Situation��5E      $��D��6)   $
��Left=�65   $��
81,425,0,0q�6P   $	��Top=�6d   
�������$��10(�6�   $��Times New Roman)�6�   ��6|   Z� 6�   � 5F   
   -   5         �Name$ 	CheckList��5F      $��-75,310,805,46q�6"   $��70��6:   $��95��6E   ���	#4C424242 J� RenderTransformOrigin$ 0.5,0.5&�6h   z�5G      V� 5H      .~�5I      �� �� 5J      �� 5K      T� 5L      5M      .+�5O      �� -   5         ��Name$ 
VaCheckBox��5O      $��Va��6+   $
��Left=�68   $��10,5,0,0q�6S   $	��Top=�6e   ���	#FFECECEC$��54��6�   $��16��6�   ���	#FF00DE03$

 Hand��6�    d� 	IsChecked$
 TrueY�6�   ��6}   Z� 6�   �� 5P      -   5        $ IaCheck��5P      $��Ia��6(   $
��Left=�65   $��	10,26,0,0q�6P   $	��Top=�6c   ���	#FFECECEC$��54��6�   $��16��6�   ���	#FFDE0000$

 Hand��6�   $
 TrueY�6�   ��6{   Z� 6�   �� 5Q      -   5        $ EaCheck��5Q      $��Ea��6(   $
��Left=�65   $��	10,47,0,0q�6P   $	��Top=�6c   ���	#FFECECEC$��54��6�   $��16��6�   ���	#FF0014FF$

 Hand��6�   $
 TrueY�6�   ��6{   Z� 6�   �� 5R      -	   5        $	 RPM��5R      $	��RPM��6$   $
��Left=�62   $��	10,68,0,0q�6M   $	��Top=�6`   ���	#FFECECEC$��54��6�   $��16��6�   ���	#FFBB8700$

 Hand��6�   $
 TrueY�6�   ��6x   Z� 6�   5S      5U      5V      