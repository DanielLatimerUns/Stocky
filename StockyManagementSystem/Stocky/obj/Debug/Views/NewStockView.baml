   M S B A M L   `   `   `  ����E  AStockyMain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Stocky.ViewModelsStocky.ViewModels  5   	   5clr-namespace:Stocky.ViewModelsStocky.ViewModels  5   	   ;"clr-namespace:Stocky.UI.ConvertersStocky.UI.Converters  5   	   7 clr-namespace:Stocky.UI.ControlsStocky.UI.Controls  5	   	       Stocky.Views.NewStockView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   1Localclr-namespace:Stocky.ViewModels     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .VMclr-namespace:Stocky.ViewModels     5   	   2Con"clr-namespace:Stocky.UI.Converters     5   	   /NT clr-namespace:Stocky.UI.Controls     5	   	     � DataContext  5      )   #Stocky.ViewModels.NewStockViewModel  5   
    � 	Resources 5      '   !Stocky.UI.Converters.BoolToColour   ValidationConverter(   "Stocky.UI.Converters.DecimalParser  DecimalParser��%:   &	        &	        5   
   6     5   
   6   5      .��5      �  5   
    �� 
TargetType  	MainLabel �� BasedOn �� Property �� Value  MainTextBox��%  '��       7  '�� r     7  �� 5      " ��6   8   .�5      ��  � ��$	 100��6*   �� 5       � ��$ 13(�6-   5      �� 5      " ��6   8   .�5      ��  � ��$ 
60,0,150,0q�5   %   �� 5       � ��$ Center=�5   %   5      5        � ColumnDefinitions 5!   
   �� 5"      $��5* �6   �� 5#      $
��789* �6    � RowDefinitions 5%   
   �� 5&      $
��0.1* �6   �� 5'      $��1* �6   .+�5)   
   �� $��5,10,0,0q�6   $��1��6'   $��1��67    5+      ��%X   ']�       �� 5,      " ]�6   .�5-      ��  � ��$ 0,15,0,0q�6/   5.      5/      .+�50      ]� .+�52      �� $��Title��6   �� 53      ��6   �� *StockDetails.Name+ �� UpdateSourceTrigger$ PropertyChanged=�	 �� ValidatesOnDataErrors	 .  54      ]� 55      .+�56      �� $��Description��6   �� 57      
 �� TextWrapping$

 Wrap=�6    �� AcceptsReturn . $	��100��5:   "   ��58   "   �� *StockDetails.Description+$ PropertyChanged=�	 .  5:   "   5=      ]� 5>      .+�5?      �� $��Category��6   �� 5@      $	��240��6   $
��Left=�5A   %   $��	100,0,0,0q�5B   %    �� DisplayMemberPath$
 Name��5E   %   ��5C   %   �� *CategoryList+ �� SelectedItem 5D   %   �� *StockDetails.CategoryObject+5E   %   5F      ]� 5H      .+�5I      �� $��
Value Band��6   �� 5J      $	��240��6   $
��Left=�5K   #   $��	100,0,0,0q�5L   #   $ Description��5O   #   ��5M   #   �� *ValueBandList+ 5N   #   �� *StockDetails.ValueBandObject+5O   #   5P      ]� 5Q      .+�5R      �� $��Purchase Cost��6   )   #Stocky.UI.Controls.skNumericTextBox  5S      $	��240��6&   $
��Left=�5T   #   $��	100,0,0,0q�5U   #      
IsCurrency . ��5W   #   �� *StockDetails.purchasedvalue+$ PropertyChanged=�	 .   �� StringFormat$ c��5X      5Y      ]� 5Z   
   $��1��6   $	��Top=�5[      $
��left=�5\      $��	10,10,0,0q�5]      .+�5^      �� -   5        $��btnSave��5^      $
��Save��5_        SaveCloseButtons#���  5b      ��5`      �� *SubmitStock+��5a      �� *StockDetails.IsObjectValid+ �� 	Converter# [  5b      �� 5c      -   5        $��btnClose��5c      $��Close��5d      #���  5f      ��5e      �� *UiCommands.CloseCurrentTab+5f      5g      5h      5i      