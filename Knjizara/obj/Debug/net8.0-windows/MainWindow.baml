   M S B A M L   `   `   `  ����C  ?Knjizara, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:KnjizaraKnjizara  5   	       Knjizara.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Knjizara     5   	     � Title$  Knjizara��5   	   $	��450��6   $
��1009��6'   .��5	      � .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ DataGridCentral��5
      $��Center=�5      $��Center=�5         SelectionUnit$ FullRow=�5         SelectionMode$ Single=�5         
IsReadOnly$
 True��5      $	��900��5         CanUserAddRows$ False��5         AlternatingRowBackground
 ��� �5      �� 5   
   $��Center=�6   $	��Top=�5      $
��auto��5      $��30��5       �� Orientation$ 
Horizontal=�5      .+�5      �� -   5         ��Name$ btnZanr��5      $
��Zanr��5      $
��Left=�5      $	��Top=�5      $��75��5       $��30��5!      6   �� 5#      -   5        $ brnAutor��5#      $��Autor��5%      $
��Left=�5&      $	��Top=�5'      $��75��5(      $��30��5)      6   �� 5+      -   5        $ brnZaposleni��5+      $��	Zaposleni��5-      $
��Left=�5.      $	��Top=�5/      $��75��50      $��30��51      6   �� 53      -   5        $ btnRacun��53      $��Racun��55      $
��Left=�56      $	��Top=�57      $��75��58      $��30��59      6   �� 5;      -   5        $ btnNarudzbina��5;      $��
Narudzbina��5=      $
��Left=�5>      $	��Top=�5?      $��75��5@      $��30��5A      6   �� 5C      -   5        $ 	btnKnjiga��5C      $��Knjige��5E      $
��Left=�5F      $	��Top=�5G      $��75��5H      $��30��5I      6   �� 5K      -   5        $ 
btnKartica��5K      $��Kartica��5M      $
��Left=�5N      $	��Top=�5O      $��75��5P      $��30��5Q      6   �� 5S      -	   5        $ btnIzdanje_racun��5S      $��Izdanje racun��5U      $
��Left=�5V      $	��Top=�5W      $��95��5X      $��30��5Y      6   �� 5[      -
   5        $ btnIzdanjeNarudzbina��5[      $��Izdanje Narudzbina��5]      $
��Left=�5^      $	��Top=�5_      $	��115��5`      $��30��5a      6   �� 5c      -   5        $ 
btnIzdanje��5c      $��Izdanje��5e      $
��Left=�5f      $	��Top=�5g      $��75��5h      $��30��5i      6   �� 5k      -   5        $ btnDobavljac��5k      $��	Dobavljac��5m      $
��Left=�5n      $	��Top=�5o      $��75��5p      $��30��5q      6   �� 5s      -   5        $ btnClan��5s      $
��Clan��5u      $
��Left=�5v      $	��Top=�5w      $��75��5x      $��30��5y      6   5}      �� 5   
   $��Center=�6   $��Bottom=�5�      $
��auto��5�      $��30��5�      $ 
Horizontal=�5�      .+�5�      �� -   5        $ btnDodaj��5�      $��Dodaj��5�      $
��Left=�5�      $	��Top=�5�      $��75��5�      $��30��5�      6   �� 5�      -   5        $ 	btnIzmeni��5�      $��Izmeni��5�      $
��Left=�5�      $	��Top=�5�      $��75��5�      $��30��5�      6   �� 5�      -   5        $ 	btnObrisi��5�      $��Obrisi��5�      $
��Left=�5�      $	��Top=�5�      $��75��5�      $��30��5�      6   5�      5�      5�      