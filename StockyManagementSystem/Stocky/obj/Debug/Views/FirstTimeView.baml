   M S B A M L   `   `   `  ����E  AStockyMain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Stocky.ViewsStocky.Views  5   	   5clr-namespace:Stocky.ViewModelsStocky.ViewModels  5   	        Stocky.Views.FirstTimeView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Stocky.Views     5   	   .VMclr-namespace:Stocky.ViewModels     5   	   $	��600��5	   	   $	��800��6     � DataContext  5      *   $Stocky.ViewModels.FirstTimeViewModel  5   
    � 	Resources 5       �� 
TargetType �� Property �� Value��%�  '��       ']� �     '�� �     �� 5   
   " ��6   .�5      ��  � ��$ 
60,0,150,0q�5   %   �� 5       � ��$ Center=�5   %   �� 5       � ��$	 400��5   )   5      �� 5   
   " ]�6   .�5      ��  � ��$ 0,40,0,0q�6'   5      �� 5   
   " ��6   .�5      ��  � ��$	 200��6&   �� 5       � ��$ 13(�6)   5      5      .��5       �  � RowDefinitions 5!   
   �� 5"      $��50 �6   �� 5#      $��* �6   �� 5$      $��35 �6   .+�5&   
   ��  �� Orientation$ 
Horizontal=�6   .+�5'      �� $��Bottom=�6   $��15(�5(      �� 5)      .�6   LJWelcome to Stocky, Please Complete the following setup before continueing.6c   5*      5+      �� 5,   
   -   5        $��
MainTabCon��5,       �� TabStripPlacement$
 Left=�6'   
�������$��1��5.       5/      ��%x   '��       �� 50      " ��6   .�51      ��  � ��$ 50��6/   �� 52       � ��
 ��ح�63   53      54      .@�55      �� $��General��6   $��0,0,-2,0q�6'   .��56      � .+�57      �� $��	10,10,0,0q�6!   $��2��64   .+�5:      �� $��General Global Settings��6$   ]� 5;      .+�5<      �� $��Photo Storage UNC path��6$   �� 5=      ��6&   �� *APP.ImageDirectory+5>      5?      5@      5A      �� 5B      $
��SSRS��6   $��0,0,-2,0q�6$   .��5C      � .+�5D      �� $��	10,10,0,0q�6!   $��2��64   .+�5F      �� $!��SSRS report server Settings��6$   ]� 5G      .+�5H      �� $�� Base Server URL��6$   �� 5I      ��6&   �� *APP.SSRSServer+5J      ]� 5K      .+�5L      �� $��SSRS database server name��6$   �� 5M      $�� ��6&   5N      ]� 5O      .+�5P      �� $��SSRS database name��6$   �� 5Q      $�� ��6&   5R      5S      5T      5U      5V      �� 5W   
   $��2��6   .+�5X      �� -   5        $��Continue��5X      $��35��5Y      ��5Z      �� *
APP.Save+62   5[      5\      5]      