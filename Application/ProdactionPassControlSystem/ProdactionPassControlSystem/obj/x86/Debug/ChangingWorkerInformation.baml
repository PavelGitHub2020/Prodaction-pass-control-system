   M S B A M L   `   `   `  ����V  RProdactionPassControlSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:ProdactionPassControlSystemProdactionPassControlSystem  5   	   ;    5ProdactionPassControlSystem.ChangingWorkerInformation   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:ProdactionPassControlSystem     5   	     � Title$  ChangingWorkerInformation��5   	   $	��460��6+   $	��880��68   
����·�$	��460��6^   $	��880��6n    � 	Resources 5	      >   8ProdactionPassControlSystem.DepartmentNameToColorConvert    DepartmentNameToColorConvert)  #System.Windows.Controls.DataGridRow �� 
TargetType �� Property �� Value �� 	Converter��%�   &	        '       7     5
   
   6-   �� 5   
   "  6   .�5      ��  � �� 6+   �� *Name1+8   5      5      .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ removeWorkerGrid��5      $��Center=�6-   $	��600��6J   $	��380��6V   $��-260,25,0,0q�6c   �� 5   
   $��20(�6   $��Center=�6"   $��
-450,0,0,0q�6?   0.To change the worker information, enter the ID6�   �� 5   
   -   5         ��Name$ 	textBoxId��5      $��20(�6%   $	��100��63   $��25��6?   $��Center=�6K   $��80,-395,0,0q�6h   6   �� 5   
   -   5         ��Name$ select��5      $��70��6!   $��25��6,   $��Center=�68   $��15(�6U   $��270,-395,0,0q�6c   .��5   -   Select65   �� 5   
   
����·�$��Center=�6/   $��50��6L   $	��350��6W   $��400,-20,0,0q�6d   .+�5      �� 
��� E��$��30��6)   $��30��64   $��10q�6@   �� 5      
���/���$��30��6+   $��30��66   $��10q�6B   �� 5      
����� �$��30��6$   $��30��6/   $��10q�6;   �� 5      
�������$��30��6%   $��30��60   $��10q�6<   �� 5      
������$��30��6*   $��30��65   $��10q�6A   �� 5      
�������$��30��6#   $��30��6.   $��10q�6:   �� 5      
��� ���$��30��6$   $��30��6/   $��10q�6;   5      �� 5    
   $��15(�6   $��Center=�6"   $	��100��6?   $��30��6K   $��550,-310,0,0q�6W   	Service M6x   �� 5!   
   $��15(�6   $��Center=�6"   $	��100��6?   $��30��6K   $��550,-210,0,0q�6W   	Service H6x   �� 5"   
   $��15(�6   $��Center=�6"   $	��100��6?   $��30��6K   $��550,-110,0,0q�6W   Traffic service6~   �� 5#   
   $��15(�6   $��Center=�6"   $	��175��6?   $��30��6K   $��630,-10,0,0q�6W   Electro-mechanical service6�   �� 5$   
   $��15(�6   $��Center=�6"   $	��105��6?   $��30��6K   $��
560,90,0,0q�6W   Security service6}   �� 5%   
   $��15(�6   $��Center=�6"   $	��150��6?   $��30��6K   $��600,190,0,0q�6W   Economic department6�   �� 5&   
   $��15(�6   $��Center=�6"   $	��150��6?   $��30��6K   $��600,290,0,0q�6W   Computer department6�   5'      5(      