   M S B A M L   `   `   `  ����E  AStockyMain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Stocky.ViewsStocky.Views  5   	   5clr-namespace:Stocky.ViewModelsStocky.ViewModels  5   	   (    "Stocky.Views.NewStockSelectionView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Stocky.Views     5   	   .VMclr-namespace:Stocky.ViewModels     5   	   $	��300��5	   	   $	��300��6   $	��300��5
   	   $	��300��6     � WindowStartupLocation$  CenterOwner=�5   	    � DataContext 5      2   ,Stocky.ViewModels.NewStockSelectionViewModel  5   
   .��5      �  � RowDefinitions 5   
   �� 5   
   $��60 �6   �� 5   
   $��* �6   �� 5   
   $��40 �6   .+�5   
   �� $
��Wrap=�6   $��10,15,10,10q�5      $��12(�5      $��Center=�5      1/Please select a stock entry method to contunue.5      �� 5   
   $��Bottom=�6   $	��260��6/   �� 5   
   $��Center=�6   $��Center=�5      $��1��5       � 	Resources 5       �� 
TargetType �� Property �� Value��%Y   '	�       �� 5       " 	�6   .�5!      ��  � ��$ 	0,10,0,10q�6/   5"      5#      .+�5$      	�  	� 	GroupName$ MB��6   $��Single Stock Item��5%       d� 	IsChecked 5&      �� *
IsSingle+	� 5'      $ MB��6   $%��Single Stock Item with purchase��5(       5)      �� *
IsSingleWP+	� 5*      $ MB��6   $%��Batch stock items with purchase��5+       5,      �� *	IsMultiWP+5.      �� 5/   
   $��2��6   $��OK��50      ��51      �� *
Continue+53      54      