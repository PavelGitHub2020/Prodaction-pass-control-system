   M S B A M L   `   `   `  ����V  RProdactionPassControlSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:ProdactionPassControlSystemProdactionPassControlSystem  5   	   (    "ProdactionPassControlSystem.Remove   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:ProdactionPassControlSystem     5   	     � Title$  Remove��5   	   $	��460��6   $
��1020��6%   
���   �$	��460��6E   $
��1020��6U    � 	Resources 5	         soundOn  soundOf  eng  ru>   8ProdactionPassControlSystem.DepartmentNameToColorConvert   DepartmentNameToColorConvert)  #System.Windows.Controls.DataGridRow �� 
TargetType �� Property �� Value �� 	Converter��%�  &	        &	 P     &	 �     &	 �     &	 M    '  `    7  �� 5
   
   $ ��ImageForSound\sound_on.png��6    $��25��6D   $��25��6P   �� 5   
   $ ��ImageForSound\sound_of.png��6    $��25��6D   $��25��6P   �� 5   
   $&�� ImageForLanguage\englishIcon.jpg��6   $��25��6F   $��25��6R   �� 5   
   $'��!ImageForLanguage\russianIcon3.jpg��6   $��25��6F   $��25��6R     5   
   6-   �� 5   
   "  6   .�5      ��  � �� 6+   �� *Name1+8   5      5      .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ removeWorkerGrid��5      $��Center=�6-   $	��600��6J   $	��380��6V   $��-390,25,0,0q�6c   �� 5   
   $��30��6   $��20(�6    $��Center=�6.   $��-720,-395,0,0q�6K   
���  ��#!To delete an worker, enter its ID5   H   �� 5   
   -   5         ��Name$ 	textBoxId��5      $��20(�6%   $	��100��63   $��25��6?   $��Center=�6K   $��-330,-395,0,0q�6h   6�   �� 5   
   -   5         ��Name$ buttonRemove��5      $��70��6'   $��25��62   $��Center=�6>   $��15(�6[   $��-150,-395,0,0q�6i     ChangeColor#��[ 5   &   .��6K   Remove6S   �� 5   
   -   5        $ sound��5      $��25��6!   $��25��6,    	 Sound#��[ 68   $��Center=�5      $��-50,-395,0,0q�6]   	 � ToolTip$	 Sound on��5      .��5      C� 
 C� ResourceKey$
 soundOn��6   5       �� 5"   
   -   5        $ language��5"      $��25��6#   $��25��6.   $��Center=�5#      $��10,-395,0,0q�6X   $	 English for sound��5$      .��5%      C� $	
 eng��6   5&      �� 5(   
   -   5        $ theWholeList��5(      $	��130��6'   $��30��63   $��Center=�6?   $��360,-320,0,0q�6\   $��15(�5)      #��[ 6<   .��6b   The whole list6r   �� 5+   
   -   5        $ serviceM��5+      $	��130��6#   $��30��6/   
��� E��$��Center=�6R   $��15(�6o   $��360,-220,0,0q�6}   .��5,   )   View separately6:   �� 5-   
   -   5        $ serviceH��5-      $	��130��6#   $��30��6/   
���/���$��Center=�6T   $��15(�6q   $��360,-120,0,0q�6   .��5.   )   View separately6:   �� 5/   
   -	   5        $ trafficServiceClick��5/      $	��130��6.   $��30��6:   
����� �$��Center=�6X   $��15(�6u   $��360,-20,0,0q�6�   .��50   /   View separately6@   �� 51   
   -
   5        $ electroMechanicalService��51      $	��130��63   $��30��6?   
�������$��Center=�6^   $��15(�6{   $��
360,80,0,0q�6�   .��52   9   View separately6J   �� 53   
   -   5        $ securityService��53      $	��130��6*   $��30��66   
������$��Center=�6Z   $��15(�6w   $��360,180,0,0q�6�   .��54   0   View separately6A   �� 55   
   -   5        $ economicDepartment��55      $	��130��6-   $��30��69   
�������$��Center=�6V   $��15(�6s   $��360,280,0,0q�6�   .��56   3   View separately6D   �� 57   
   -   5        $ computerDepartment��57      $	��130��6-   $��30��69   
��� ���$��Center=�6W   $��15(�6t   $��360,370,0,0q�6�   .��58   3   View separately6D   �� 5:   
   -   5         ��Name$ all_Services��5:      $��15(�6*   $��Center=�68   $	��100��6U   $��20��6a   $��650,-320,0,0q�6m   
�������All sevices5;   4   �� 5=   
   -   5        $ 	service_M��5=      $��15(�6'   $��Center=�65   $	��100��6R   $��20��6^   $��650,-220,0,0q�6j   
��� E��	Service M5>   6   �� 5?   
   -   5        $ 	service_H��5?      $��15(�6'   $��Center=�65   $	��100��6R   $��20��6^   $��650,-120,0,0q�6j   
���/���	Service H5@   8   �� 5A   
   -   5        $ traffic_Service��5A      $��15(�6-   $��Center=�6;   $	��100��6X   $��20��6d   $��650,-20,0,0q�6p   
����� �Traffic service5B   7   �� 5C   
   -   5        $  electro_Mechanical_Service��5C      $��15(�68   $��Center=�6F   $	��175��6c   $��20��6o   $��
730,80,0,0q�6{   
�������Electro-mechanical service5D   C   �� 5E   
   -   5        $ security_Service��5E      $��15(�6.   $��Center=�6<   $	��105��6Y   $��20��6e   $��660,180,0,0q�6q   
������Security service5F   >   �� 5G   
   -   5        $ economic_Department��5G      $��15(�61   $��Center=�6?   $	��150��6\   $��20��6h   $��700,280,0,0q�6t   
�������Economic department5H   :   �� 5I   
   -   5        $ computer_Department��5I      $��15(�61   $��Center=�6?   $	��150��6\   $��20��6h   $��700,366,0,0q�6t   
��� ���Computer department5J   ;   5K      5L      