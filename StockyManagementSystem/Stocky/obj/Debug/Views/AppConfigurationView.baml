   M S B A M L   `   `   `  ����E  AStockyMain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Stocky.ViewsStocky.Views  5      5clr-namespace:Stocky.ViewModelsStocky.ViewModels  5      7 clr-namespace:Stocky.ApplicationStocky.Application  5      '    !Stocky.Views.AppConfigurationView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:Stocky.Views     5      .VMclr-namespace:Stocky.ViewModels     5      0APP clr-namespace:Stocky.Application     5        � DataContext  5      1   +Stocky.ViewModels.AppConfigurationViewModel  5   
    � 	Resources 5      ,   &Stocky.Application.ApplicationSettings   ApplicationSettings �� 
TargetType �� Property �� Value��%�  &	        '��      ']� �     '�� 
      5   
   6"   �� 5   
   " ��6   .�5      ��  � ��$ 
60,0,150,0q�5   %   �� 5       � ��$ Center=�5   %   �� 5       � ��$	 400��5   )   5      �� 5   
   " ]�6   .�5      ��  � ��$ 0,40,0,0q�6'   5      �� 5   
   " ��6   .�5      ��  � ��$	 200��6&   �� 5       � ��$ 13(�6)   5      5       .��5!      �  � RowDefinitions 5"   
   �� 5#      $��30 �6   �� 5$      $��* �6   .+�5&   
   c� 
������� 5(      ��%X   '��       �� 5)      " ��6   .�5*      ��  � ��$ 20,0,2,0q�6/   5+      5,      .@�5-      �� $
��Save��6   ��6$   ��  �� Source# [   �� Path$
 Save��� 5.      $��Close��6   ��6%   �� *UiCommands.CloseCurrentTab+�� 5/      $��	20,0,20,0q�6   50      �� 51   
   -   5        $��
MainTabCon��51       �� TabStripPlacement$
 Left=�6'   
�������$��1��53       54      ��%x   '��       �� 55      " ��6   .�56      ��  � ��$ 50��6/   �� 57       � ��
 ��ح�63   58      59      .@�5:      �� $��General��6   $��0,0,-2,0q�6'   .��5;      � .+�5<      �� $��	10,10,0,0q�6!   $��2��64   .+�5?      �� $��General Global Settings��6$   ]� 5@      .+�5A      �� $��Photo Storage UNC path��6$   �� 5B      ��6&   �� # [  $ ImageDirectory�5C      ]� 5D      .+�5E      �� $��Export Directory��6$   �� 5F      ��6&   �� # [  $ ExportDirectory�5G      5H      5I      5J      �� 5K      $
��SSRS��6   $��0,0,-2,0q�6$   .��5L      � .+�5M      �� $��	10,10,0,0q�6!   $��2��64   .+�5O      �� $!��SSRS report server Settings��6$   ]� 5P      .+�5Q      �� $�� Base Server URL��6$   �� 5R      ��6&   �� # [  $ 
SSRSServer�	 �� UpdateSourceTrigger$	 PropertyChanged=�5S      ]� 5T      .+�5U      �� $��SSRS database server name��6$   �� 5V      $�� ��6&   5W      ]� 5X      .+�5Y      �� $��SSRS database name��6$   �� 5Z      $�� ��6&   5[      5\      5]      5^      5`      5a      5b      