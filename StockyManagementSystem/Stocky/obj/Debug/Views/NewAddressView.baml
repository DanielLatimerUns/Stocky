   M S B A M L   `   `   `  ����E  AStockyMain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Stocky.ViewsStocky.Views  5      5clr-namespace:Stocky.ViewModelsStocky.ViewModels  5      !    Stocky.Views.NewAddressView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:Stocky.Views     5      .VMclr-namespace:Stocky.ViewModels     5        � DataContext  5
      +   %Stocky.ViewModels.NewAddressViewModel  5   
   .��5      �  � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $	��500 �6   .+�5   
   c� 
������� � 	Resources 5       �� 
TargetType �� Property �� Value��%X   '��       �� 5      " ��6   .�5      ��  � ��$ 20,0,2,0q�6/   5      5      .@�5      �� $
��Save��6   ��6$   �� *SubmitNewAddressCommand+�� 5      $��Close��6   ��6%   �� *UiCommands.CloseCurrentTab+�� 5      $��	20,0,20,0q�6   5      �� 5   
   
�������$��1��5      $��2��6"   .+�5       �  � ColumnDefinitions 5!      �� 5"      $��2* �6'   �� 5#      $	��13* �6'   .+�5%      �� $��1��6   $��2��6,   .��5&      �� $��5,10,0,0q�6!    5(      ��%Y  '��       ']� �     '�� �     �� 5)      " ��6$   .�5*   "   ��  � ��$ 
60,0,150,0q�5+   %   �� 5,   "    � ��$ Center=�5-   %   5.      �� 5/      " ]�6$   .�50   "   ��  � ��$ 0,15,0,0q�6;   51      �� 52      " ��6$   .�53   "   ��  � ��$	 100��6:   �� 54   "    � ��$ 13(�6=   55      56      .+�58      ]� .+�59      �� $��AddressLine 1��6$   �� 5:      ��6&   �� *AddressObject.AddressLine1+ �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnDataErrors .  5;      ]� 5<      .+�5=      �� $��AddressLine 2��6$   �� 5>      	 �� TextWrapping$
	 Wrap=�6&   
 �� AcceptsReturn
 . ��5?   "   �� *AddressObject.AddressLine2+5@   "   5A      ]� 5B      .+�5C      �� $��AddressLine 3��6$   �� 5D      $
	 Wrap=�6&   
 . ��5E   "   �� *AddressObject.AddressLine3+5F   "   5G      ]� 5H      .+�5I      �� $��	Post Code��6$   �� 5J      $
	 Wrap=�6&   
 . ��5K   "   �� *AddressObject.PostCode+$ PropertyChanged=� .  5L   "   5M      ]� 5N      .+�5O      �� $
��Town��6$   �� 5P      $
	 Wrap=�6&   
 . ��5Q   "   �� *AddressObject.Town+5R   "   5S      ]� 5T      .+�5U      �� $��County��6$   �� 5V      $
	 Wrap=�6&   
 . ��5W   "   �� *AddressObject.County+5X   "   5Y      ]� 5Z      .+�5[      �� $��Country��6$   �� 5\      $
	 Wrap=�6&   
 . ��5]   "   �� *AddressObject.Country+$ PropertyChanged=� .  5^   "   5_      5`      5a      5b      5c      5d      5e      