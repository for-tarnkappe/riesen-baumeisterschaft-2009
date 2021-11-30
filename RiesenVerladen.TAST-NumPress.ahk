Quantity := 1

;Numpad 1-6 mit Hotkey belegen und Funktion aufrufen
Hotkey, NumPad1, Number1
Hotkey, NumPad2, Number2
Hotkey, NumPad3, Number3
Hotkey, NumPad4, Number4
Hotkey, NumPad5, Number5
Hotkey, NumPad6, Number6

;Buchstaben um die gesetzte Anzahl zu laden
Hotkey, a, placeSteine
Hotkey, s, placeBretter
Hotkey, d, placeSand
Hotkey, f, placeZement

;Mit der Leertaste den Auftrag abschließen
Hotkey, Space, sendAuftrag

return

Number1:
	Quantity := 1
return
Number2:
	Quantity := 2
return
Number3:
	Quantity := 3
return
Number4:
	Quantity := 4
return
Number5:
	Quantity := 5
return
Number6:
	Quantity := 6
return

placeSteine:
Loop %Quantity% { ;x-Mal anklicken
	MouseClick, left,  414,  517
}
Quantity := 1
return
placeBretter:
Loop %Quantity% { ;x-Mal anklicken
	MouseClick, left,  580,  455
}
Quantity := 1
return
placeSand:
Loop %Quantity% { ;x-Mal anklicken
	MouseClick, left,  779,  526
}
Quantity := 1
return
placeZement:
Loop %Quantity% { ;x-Mal anklicken
	MouseClick, left,  1155,  510
}
Quantity := 1
return

sendAuftrag:
MouseClick, left,  1080,  311 ;Auftrag abschließen
return
