   M S B A M L   `   `   `  ����E  ACalculator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Calculator
Calculator  5   	       Calculator.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Calculator     5   	   -   5          � Title$  
MainWindow��5	   	   $	��850��6   $	��850��6)   .��5
      � 
����ح�.+�5   
   �� -   5         ��Name$ 
BackToMenu��5   4   $��10q�6   $��65��6   $��30��6(   $
��Menu��6H   $
��Left=�6j   $	��Top=�6�   $y�	Collapsed=�6�   �� 5   
   -   5        $ ClearResultPane��5   4   $��10q�6   $��65��6   $��30��6(   $��Clear��6M   $��Right=�6p   $	��Top=�6�   $y�	Collapsed=�6�   �� 5   
   -   5        $ GoToA��5   4   $��10q�6   $��65��6   $��30��6(   $��MatrixA��6C   $
��Left=�6h   $	��Top=�6�   $y�	Collapsed=�6�   �� 5   
   -   5        $ 
GoToResult��5   )   $��10q�6   $��30��6   $��Results History��6=   $��Right=�6j   $	��Top=�6�   $y�Visible=�6�   �� 5   
   -   5         ��Name$ 	MenuPanel��5       �� Orientation$ Vertical=�6(   $��Center=�6?   $��Center=�6\   .+�5      �� -   5        $ 	MatrixAdd��5   b   $	��200��6   $��55��6!   $��12(�6-   $��Matrix Calculator��6;   $��5q�6W   6u   �� 5      -   5        $ SysCalc��5   o   $	��200��6   $��55��6!   $��12(�6-   $$��System of Equations calculator��6;   $��5q�6d   6�   �� 5      -	   5        $ 
SimpleCalc��5   b   $	��200��6   $��55��6!   $��12(�6-   $��Simple calculator��6;   $��5q�6W   6v   5      � 5   
   -
   5         �Name$
 Calc��5      $y�	Collapsed=�6   .+�5      �� $��Center=�6   $��Center=�66   .+�5      �� $ Vertical=�6   
���P��.+�5      �� -   5         ��Name$ ResultTextBox��5   0   ${�False��6   $��50��6f   $	��Top=�6r   $��24(�6�    �� TextAlignment$ Right=�6�   $��Center=�6�   ��6G   �� *CurrentNumber+6�   � 5      $��1��6   .+�5      �� $ 
Horizontal=�6%   .+�5      �� -   5        $��3q�5   %   $��75��60   $��50��6;   $��7��6G   6S   �� 5      -   5        $��3q�5   %   $��75��60   $��50��6;   $��8��6G   6S   �� 5      -   5        $��3q�5   %   $��75��60   $��50��6;   $��9��6G   6S   �� 5      -   5        $��3q�5   %   $��75��60   $��50��6;   $��/��6G   6S   5      5      � 5       $��2��6   .+�5!      �� $ 
Horizontal=�6%   .+�5"      �� -   5        $��3q�5"   %   $��75��60   $��50��6;   $��4��6G   6S   �� 5#      -   5        $��3q�5#   %   $��75��60   $��50��6;   $��5��6G   6S   �� 5$      -   5        $��3q�5$   %   $��75��60   $��50��6;   $��6��6G   6S   �� 5%      -   5        $��3q�5%   %   $��75��60   $��50��6;   $��*��6G   6S   5&      5'      � 5(      $��3��6   .+�5)      �� $ 
Horizontal=�6%   .+�5*      �� -   5        $��3q�5*   %   $��75��60   $��50��6;   $��1��6G   6S   �� 5+      -   5        $��3q�5+   %   $��75��60   $��50��6;   $��2��6G   6S   �� 5,      -   5        $��3q�5,   %   $��75��60   $��50��6;   $��3��6G   6S   �� 5-      -   5        $��3q�5-   %   $��75��60   $��50��6;   $��-��6G   6S   5.      5/      � 50      $��4��6   .+�51      �� $ 
Horizontal=�6%   .+�52      �� -   5        $��3q�52   %   $��75��60   $��50��6;   $��.��6G   6S   �� 53      -   5        $��3q�53   %   $��75��60   $��50��6;   $��0��6G   6S   �� 54      -   5        $��3q�54   %   $��75��60   $��50��6;   $��=��6G   6S   �� 55      -   5        $��3q�55   %   $��75��60   $��50��6;   $��+��6G   6S   �� 56      -   5        $��75��56   %   $��50��60   $��C��6<   6H   57      58      59      5:      5;      � 5<   
   -   5        $	 SOE��5<      $y�	Collapsed=�6   .+�5=      �� -   5        $ SSizes��5=      $ 
Horizontal=�6)   $��	0,125,0,0q�6B   $��Center=�6U   $	��Top=�6r   .+�5>      �� -   5        $ RowAddS��5>   0   $��35��6   $��2q�6%   $��35��6A   .��6`   Row+6f   �� 5?      -    5        $ 
RowRemoveS��5?   0   $��35��6   $��2q�6%   $��35��6D   .��6c   Row-6i   �� 5@      -!   5        $ ColAddS��5@   0   $��35��6   $��2q�6%   $��35��6A   .��6`   Col+6f   �� 5A      -"   5        $ 
ColRemoveS��5A   0   $��35��6   $��2q�6%   $��35��6D   .��6c   Col-6i   �� 5B      -#   5        $��35��5B      $��15,0,0,0q�6%   $	��100��67   .��6Z   Show Result6g   5C      � 5D      $��	0,175,0,0q�6   .+�5E      � -$   5        $ MatrixS��5E      .+�5F      �� .+�5G      �� $��Center=�6%   $ 
Horizontal=�6B   .+�5H      �� -%   5        $ ShowMatrixS��5H   )   $��0,25,0,0q�6>   $ Vertical=�6P   �� 5I      -&   5         ��Name$ dividerLine��5I   K   $��25q�6#   	 �� X1$ 0��6/   		 �� Y1$	 0��66   	
 �� X2$
 0��6=   	 �� Y2$ 0��6D   
���   �$��1��6o   �� 5J      -'   5        $ ListOfEq��5J   )   $��0,25,0,0q�6;   $ Vertical=�6M   5K      5L      5M      5N      5O      � 5P   
   -(   5        $ 
MatrixCalc��5P      $y�	Collapsed=�6#   .+�5Q      �� $ 
Horizontal=�6   .+�5R      � -)   5        $ MatrixA��5R      $��Center=�6(   $	��850��6E   $y�Visible=�6Q   .+�5S      �� .+�5T      �� -*   5        $ ASizes��5T   %   $	��140��65   $ 
Horizontal=�6A   $��
50,50,50,0q�6Z   .+�5U      �� -+   5        $ RowAddA��5U   1   $��35��6%   $��35��6B   .��6a   Row+6g   �� 5V      -,   5        $ 
RowRemoveA��5V   %   $��35��69   .��6X   Row-6^   �� 5W      --   5        $ ColAddA��5W   %   $��35��66   .��6U   Col+6[   �� 5X      -.   5        $ 
ColRemoveA��5X   %   $��35��69   .��6X   Col-6^   5Y      �� 5Z      $��Center=�6$   $��0,25,0,0q�6A   
Matrix A6]   �� 5[      -/   5        $ ShowMatrixA��5[   B   $��Center=�6%   $��25q�6W   $ Vertical=�6c   �� 5]      -0   5        $ OperationsOnA��5]   B   $��Center=�6%   $��25q�6Y   .+�5^      �� $ 
Horizontal=�6)   .+�5_   "   �� -1   5        $ DeterminantA��5_   A   $��30��6)   $��5q�65   $	��150��6W   .��6w   Determinant6�   �� 5`   "   -2   5        $ InverseA��5`   @   $��30��6)   $��5q�65   $	��150��6R   .��6r   	Inverse6{   5a      �� 5b      $ 
Horizontal=�6)   .+�5c   "   �� -3   5        $ 
TransposeA��5c   @   $��30��6)   $��5q�65   $	��150��6T   .��6t   	Transpose6   �� 5d   "   -4   5        $ RankA��5d   @   $��30��6)   $��5q�65   $	��150��6O   .��6o   Rank6u   5e      �� 5f      $ 
Horizontal=�6)   .+�5g   "   �� -5   5        $ MultiplyByA��5g   @   $��30��6)   $��5q�65   $	��115��6U   .��6u   Multiply by6�   �� 5h   "   -6   5        $ MultiplyNumA��5h   M   $ Center=�6*   $��30��6A   $��35��6c   .��6n   26q   �� 5i   "   -7   5        $ 	DivideByA��5i   @   $��30��6)   $��5q�65   $	��115��6S   .��6s   	Divide by6~   �� 5j   "   -8   5        $ 
DivideNumA��5j   M   $ Center=�6*   $��30��6A   $��35��6a   .��6l   26o   5k      �� 5l      $ 
Horizontal=�6)   .+�5m   "   �� -9   5        $ RowEchelonFormA��5m   @   $��30��6)   $��5q�65   $	��150��6Y   .��6y   Row Echelon form6�   �� 5n   "   -:   5        $ RaiseToThePowerOfA��5n   @   $��30��6)   $��5q�65   $	��115��6\   .��6|   Raise to the power of6�   �� 5o   "   -;   5        $ RaiseByA��5o   M   $ Center=�6*   $��30��6A   $��35��6_   .��6j   26m   5p      �� 5q      -<   5        $ GoToB��5q   %   $��Go to MatrixB��64   $��0,10,0,0q�6L   $	��210��6q   $��50��6}   $��Center=�6�   5s      5t      5u      � 5w      -=   5        $ MatrixB��5w      $��Center=�6(   $	��850��6E   $y�	Collapsed=�6Q   .+�5x      �� .+�5y      �� ->   5        $ BSizes��5y   B   $��Center=�6%   $ 
Horizontal=�6S   $��
50,50,50,0q�6m   .+�5z      �� -?   5        $ RowAddB��5z   1   $��35��6%   $��35��6B   .��6a   Row+6g   �� 5{      -@   5        $ 
RowRemoveB��5{   %   $��35��69   .��6X   Row-6^   �� 5|      -A   5        $ ColAddB��5|   %   $��35��66   .��6U   Col+6[   �� 5}      -B   5        $ 
ColRemoveB��5}   %   $��35��69   .��6X   Col-6^   5~      �� 5      $��Center=�6$   $��0,25,0,0q�6A   
Matrix B6]   �� 5�      -C   5        $ ShowMatrixB��5�   B   $��Center=�6%   $��25q�6W   $ Vertical=�6c   �� 5�      -D   5        $ OperationsOnB��5�   B   $��Center=�6%   $��25q�6Y   .+�5�      �� $��Center=�6)   $ 
Horizontal=�6F   .+�5�   "   �� -E   5        $ AddMatrices��5�   )   $��50��6>   $��35��6I   $��5q�6U   .��6}   A+B6�   �� 5�   "   -F   5        $ SubstractMatrices��5�   )   $��50��6D   $��35��6O   $��5q�6[   .��6�   A-B6�   �� 5�   "   -G   5        $ MultiplyMatrices��5�   )   $��50��6C   $��35��6N   $��5q�6Z   .��6�   AxB6�   5�      5�      5�      5�      5�      5�      �� 5�   
   -H   5        $ ResultPanel��5�   ,   $ Vertical=�6   $��50q�6A   $y�	Collapsed=�6M   .+�5�      �� $
��Auto=�6   $
��Auto=�6@   $	��700��6c   $	��700��6p   .��5�      �� -I   5        $ ShowResults��5�      5�      5�      5�      5�      