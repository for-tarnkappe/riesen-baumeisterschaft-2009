RiesenWindow := "RIESEN Baumeisterschaft 2009 | Das RIESEN Gewinnspiel"
	
LButton:: ;Fange den linken Mausklick ab
	WinWait, %RiesenWindow%, ;Warte bis Spiel geöffnet
	;Wenn nicht im Vordergrund, hole es in den Vordergrund
	IfWinNotActive, %RiesenWindow%, , WinActivate, %RiesenWindow%, 
	WinWaitActive, %RiesenWindow%, ;Warte bis im Vordergrund
	
	MouseGetPos, xpos, ypos ;Speichere Mausposition
	MouseClick, left,  xpos, ypos ;Klicke an vorgesehene Mausposition
	MouseClick, left,  438,  606 ;Klicke auf Mörtel
	MouseMove,  xpos, ypos ;Bewege Maus zurück zur ursprünglicher Position
return
