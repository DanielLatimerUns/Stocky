   M S B A M L   `   `   `  ����E  AStockyMain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Stocky.UI.ControlsStocky.UI.Controls  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089)    #Stocky.UI.Controls.skNumericTextBox  �� 
TargetType �� BasedOn �� Property �� Value �� 
TargetType)   #Stocky.UI.Controls.skMainTabControl   TabItem.Selected.Background  TabItem.Selected.Border �Name J� SnapsToDevicePixels-  'System.Windows.Input.KeyboardNavigation   TabNavigation � ColumnDefinitions	 ��Name
 � RowDefinitions ��Name ��Name K� IsItemsHost   TabIndex K� ZIndex ��Name   DirectionalNavigation ��Name �� Triggers P� Property P� Value �� 
TargetName �� DataType  TabControlButtons   CloseTab �� RelativeSource �� AncestorType �� Path�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      'NT clr-namespace:Stocky.UI.Controls  5      %  '         7��'  �     7   7  &	 u    �� 5      "    6   8   .�5	   
   ��  � �� 5
      �� 5      "   6"   .R�5      �� #��z��6   #��z��5      #��z��5      5      5      5      5      �� 5      "   6   .�5   
   ��  � ��$ 2q�6$   �� 5   
    � ��$ Center=�67   �� 5   
    � ��$ Center=�65   �� 5   
    � ��8   6'   �� 5   
    � ��8  6(   �� 5   
    � ��$ 1q�6,   �� 5   
    � ��# � ��6'   �� 5   
    � �� 5      �� 5      "  6"   .R�5       � $ templateRoot��6   }�.  . $ Local=�6`    5!      �� 5"      $	 ColumnDefinition0��6/   �� 5#      $	 ColumnDefinition1��6/   $��0 �6J   
 5%      �� 5&      $ RowDefinition0��6,   $
��Auto �6D   �� 5'      $ RowDefinition1��6,   $��* �6D   .+�5)      �� $ headerPanel��6#   
������ $��0��6Q    . $��2,2,2,0q�6t   $��0��6�   $ 1��6�   $ 1��6�   �� 5*      $ contentPanel��6!   #��z��67   #��z��6c   #��z��6�   $��0��6�   $ 	Contained=�6�   $��1��6  $ 2��6  $ Local=�63  ._�5+      �� $ PART_SelectedContentHost��6/   $��SelectedContent��6Q   #��z��6q   # z 6�   5,      5-       5.      P� 5/       �@��TabStripPlacement$ Bottom=�6?   . �50      �� $ headerPanel��69    � ��$ 1��6R   �� 51      $ contentPanel��69    � ��$ 0��6S   �� 52      $ RowDefinition0��67    � ��$ * �6S   �� 53      $ RowDefinition1��67    � ��$
 Auto �6S   �� 54      $ headerPanel��67    � ��$ 2,0,2,2q�6P   55      P� 56       �@��TabStripPlacement$
 Left=�6?   . �57      �� $ headerPanel��69    � ��$ 0��6R   �� 58      $ contentPanel��69    � ��$ 0��6S   �� 59      $ headerPanel��6<    � ��$ 0��6U   �� 5:      $ contentPanel��6<    � ��$ 1��6V   �� 5;      $ ColumnDefinition0��66    � ��$
 Auto �6U   �� 5<      $ ColumnDefinition1��66    � ��$ * �6U   �� 5=      $ RowDefinition0��67    � ��$ * �6S   �� 5>      $ RowDefinition1��67    � ��$ 0 �6S   �� 5?      $ headerPanel��67    � ��$ 2,2,0,2q�6P   5@      P� 5A       �@��TabStripPlacement$ Right=�6?   . �5B      �� $ headerPanel��69    � ��$ 0��6R   �� 5C      $ contentPanel��69    � ��$ 0��6S   �� 5D      $ headerPanel��6<    � ��$ 1��6U   �� 5E      $ contentPanel��6<    � ��$ 0��6V   �� 5F      $ ColumnDefinition0��66    � ��$ * �6U   �� 5G      $ ColumnDefinition1��66    � ��$
 Auto �6U   �� 5H      $ RowDefinition0��67    � ��$ * �6S   �� 5I      $ RowDefinition1��67    � ��$ 0 �6S   �� 5J      $ headerPanel��67    � ��$ 0,2,2,2q�6P   5K      P� 5L       � {� .  . �5M      �� $ templateRoot��6G   # � ��6a    � ��5N      5P      5R      �� 5S   
    � �� 5T      �� 5U      "  ��6   .�5V      ��  � �� 5W      �� 5X      $ TabItem��6,   .R�5Y   "   ]� .+�5Z   &   �� $��Right=�6.   $��5,0,0,0q�6E   #���  6V   #��Z 6�   .��5[   *   �� $1��+/Stocky;component/Resources/CloseButton.png��60   $��20��6e   $��20��6q   6   5\   '   �� 5]   &   $��Center=�6�   ��60   ��  �� " ��$ Header�6�   5^   #   5_      5a      5b      5d      5e      �� 5f      "  ��6   .�5g   
   ��  � ��
 ���� 6'   �� 5h   
    � ��
 ���� 6(   5i      5k      