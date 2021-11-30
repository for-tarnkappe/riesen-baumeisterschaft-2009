CurSlot := 0 ;Aktueller Warenslot (Stein, Brett, Sand, Zement)

;Numpad 0-6 mit Hotkey belegen und Funktion aufrufen
Hotkey, NumPad0, Number0
Hotkey, NumPad1, Number1
Hotkey, NumPad2, Number2
Hotkey, NumPad3, Number3
Hotkey, NumPad4, Number4
Hotkey, NumPad5, Number5
Hotkey, NumPad6, Number6

;Mit der Leertaste den Auftrag abschließen
Hotkey, Space, sendAuftrag

return

Number0:
	Quantity := 0
	Goto, Jumper
return
Number1:
	Quantity := 1
	Goto, Jumper
return
Number2:
	Quantity := 2
	Goto, Jumper
return
Number3:
	Quantity := 3
	Goto, Jumper
return
Number4:
	Quantity := 4
	Goto, Jumper
return
Number5:
	Quantity := 5
	Goto, Jumper
return
Number6:
	Quantity := 6
	Goto, Jumper
return

Jumper:
	if CurSlot < 4 {
		CurSlot += 1 ;nächste Slot
		if CurSlot = 1 { ;Steine
			Loop %Quantity% { ;x-Mal anklicken
				MouseClick, left,  414,  517
			}
		}
		if CurSlot = 2 { ;Bretter
			Loop %Quantity% { ;x-Mal anklicken
				MouseClick, left,  580,  455
			}
		}
		if CurSlot = 3 { ;Sand
			Loop %Quantity% { ;x-Mal anklicken
				MouseClick, left,  779,  526
			}
		}
		if CurSlot = 4 { ;Zement
			Loop %Quantity% { ;x-Mal anklicken
				MouseClick, left,  1155,  510
			}
		}
		
	}
return

sendAuftrag:
	CurSlot := 0
	MouseClick, left,  1080,  311 ;Auftrag abschließen
return
