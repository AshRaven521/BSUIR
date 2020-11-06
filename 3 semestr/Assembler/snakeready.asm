.model small
;сегмент стека размером 256 байт
.stack 100h

.data					
;Храним координаты тела змейки(инциализируем как конкретные значения(h-шестнадцатеричная система))
snake	dw 0101h
	    dw 0102h
	    dw 0103h
	    dw 0104h
	    dw 0105h
	    dw 7CCh dup('?')
	
		
DelayTime dw  0FFFFh		
Score db 0
Score_ db 0
LvL db 0
;Директива equ заменяет во всей программе слово на заданное значение(define в си)
UpSpeed equ 48h ; Up key
DownSpeed equ 50h ; Down key
MoveUp equ 11h ; W key
MoveDown equ 1Fh ; S key(1F-аттрибут символа)
MoveLeft equ 1Eh ; A key
MoveRight equ 20h ; D key
Exit equ 01h ; ESC key
StartPosition dw 0 ; позиция символа на экране
BorderSize equ 16h
BorderLenght equ 15h
; строка " 00:00 " с атрибутом символа 1Fh (белый на синем фоне)
OutputLine db ' ',1Fh

OutputLineSize equ 14

.code

start:
	mov ax,@data
	mov ds,ax
	mov es,ax

	;Очищаем игровое поле(устанавливаем видеорежим AH=00,AL=03(80×25 стандартный 16-цветный текстовый режим))
	mov ax,0003h
	;Прерывание, предоставляющее видеосервис
	int	10h 
	

	;рисуем поле вверх
	xor bx,bx
	;0b800h соответствует сегменту дисплея в текстовом режиме(сегментный адрес текстового буфера)
	mov ax,0B800h
	mov es,ax
	;31h-в таблице ASCII соответствует 1
	mov ah,31h
	
	mov dh,00100000b
	mov dl,0B2h
	
	;Управляющие сиимволы — символы в кодировке, которым не приписано графическое представление, но которые используются для управления устройствами, организации передачи данных и других целей

	;Используется для непрерывной передачи данных
	mov cx,BorderSize
up_:	
	mov word ptr es:[bx],dx
	add bx,2
	loop up_

	mov cx,10h
draw:

	add bx,116 ;рисуем левый борт
	mov word ptr es:[bx],dx
	
	push cx	;запоминаем сколько еще надо рисовать
	
	mov dh,01000100b ;устанавливаем маску на цветной фон
	mov dl,020h
	mov cx,14h
	
color:

	add bx,2
	mov word ptr es:[bx],dx
	
	loop color	

	
	pop cx
	
	mov dh,00100000b
	mov dl,0B2h
	
	add bx,2
	mov word ptr es:[bx],dx
	
	add bx,2
	loop draw	
	
	add bx,116
	
	mov dl,Score
	add dl,'0'
	
	;Операция word ptr указывает ассемблеру, что данный операнд в памяти нужно интерпретировать как слово
	mov word ptr es:[bx],dx
	add bx,2
	mov word ptr es:[bx],dx
	add bx,2
	
	mov dl,0B2h
	;На терминалах временно приостанавливает вывод данных
	mov cx,13h
	
down_:	
	mov word ptr es:[bx],dx
	add bx,2
	loop down_
	
	mov word ptr es:[bx],dx
	add bx,2	
	
	xor ax, ax
	xor bx, bx
	xor cx, cx 
	xor dx, dx
	
	;Устанавливаем изначальное положение курсора
	mov ax,0200h
	;Первая строка, первый столбец		
	mov dx,0101h
	int 10h	

	;Количество символов в изначальной змейке(5- число повторений символа)
	mov cx,5
	;В bl записываем аттрибут символа
	mov bl,01000010b
	;Выводим символ с заданным аттрибутом на экран
	mov ax,092Ah 
	int 10h ;Выводим змейку из 5 символов "*"

		

	mov si,8 ;Индекс координаты символа головы
	xor di,di ;Индекс координаты символа хвоста
	mov cx,0001h ;Регистр cx используем для управления головой. При сложении от значения cx будет изменяться координата x или y

	;7h-звуковой сигнал
	mov bl,7h
    call AddFood

main: ;Основной цикл
	call Delay
	call KeyPress
	
    xor bh,bh
	mov ax,[snake+si] ;Берем координату головы из памяти
	add ax,cx ;Изменяем координату x
	;Если не увеличивать регистр si 2 раза змейка будет зависать
	inc si				
	inc si
	mov [snake+si],ax ;Заносим в память новую координату головы змеи
	mov dx,ax			
	mov ax,0200h
	int 10h ;Вызываем прерывание. Перемещаем курсор
	
	mov ax,0800h
    int 10h ;Читает символ 
	call CheckBoard
    call GameOver
	mov ax,0800h
    int 10h  
    cmp al,24h
	jne next
	call AddFood
	add Score,1
	cmp Score,10
	je next_score
back:
	mov ax,0200h ;счет
	;Вызываем 11 функцию 10 прерывания с аттрибутом 1
	mov dx,1101h
	int 10h 
	
	mov ah,02h
	mov dl,Score
	add dl,'0'
	int 21h
	jmp main
    mov dh,al
	
next_score:
	
	add Score_,1
	mov ax,0200h ;счет
	mov dx,1100h
	int 10h 
	
	mov ah,02h 
	mov dl,Score_
	add dl,'0'
	int 21h
	
	mov Score,0
	
	jmp back
	
next_:
	
	mov cx,OutputLineSize ; число байт в строке - в СХ
    push 0B800h
    pop es ; адрес в видеопамяти
    mov di,word ptr StartPosition ; в ES:DI
    mov si,offset OutputLine ; адрес строки в DS:SI
	;Устанавливает флаг DF=0
    cld
	;rep выполеяет заданную операцию и уменьшает cx на 1, movsb копирует байт из ds:si в es:di
    rep movsb   
		
		
next:
	
	push cx 
	
	mov cx,1
	mov bl,01000010b
	;9 функция 10 прерывания, выводящая символ с ASCII-кодом 2A
	mov ax,092Ah 
	int 10h 	
	
	pop cx ;Прерывание выводит символ '*'
	
	mov ax,0200h 		
	mov dx,[snake+di]
	int 10h
	mov ax,0200h
	mov dl,0020h
	int 21h ;Выводим пробел, тем самым удаляя хвост
	inc di
	inc di
jmp main

Delay proc

    push cx
	mov cx,0
	mov dx,DelayTime ; пауза в микросекундах
    mov ah,86h ; функция задержки
	;15 прерывание обрабатывает специфическии функции ат
	int 15h 
	mov cx,0
	mov dx,DelayTime ; пауза в микросекундах
    mov ah,86h
	int 15h
	mov cx,0
	mov dx,DelayTime ; пауза в микросекундах
    mov ah,86h
	int 15h
	pop cx
	ret
Delay endp

;Процедура для проверки на выход змейки за пределы поля
CheckBoard proc 
	cmp dl,BorderLenght;(Номер столбца)
	;Будет осуществлен переход, если не равны(ZF=0)
	jne check_left
	mov dl,01h
	;Оператор безусловного перехода
	jmp check_cur
check_left:	
	cmp dl,0
	jne check_up
	mov dl,14h
	jmp check_cur
check_up:
	cmp dh,0;(Номер строки)
	jne check_down
	mov dh,10h
	jmp check_cur
check_down:
	cmp dh,11h
	jne check_ret
	mov dh,01h
	jmp check_cur
check_cur:
	;Используем 2 функцию 10 прерывания(устанавливаем положение курсора)
	mov ax,0200h
	mov [snake+si],dx
	int 10h 
check_ret:
	ret
CheckBoard endp 

GameOver proc
		
	cmp al,2Ah ;Проверяем символ *
	;Переход будет осуществлен, если равно(ZF=1)
	je the_end
	jmp good
	
the_end: 
	;Выход из программы
    mov ax,4c00h
    int 21h
	
good:
	ret
GameOver endp

KeyPress proc
	mov ax,0100h
	int 16h	;проверка наличия символа в буфере
	jz buff_en ;Без нажатия выходим
	;Обнуляем регистр AH, значит будем использовать функцию 0 21 прерывания(чтение символа с ожиданием)
	xor ah,ah
	int 16h
	cmp ah,MoveDown
	jne up
	cmp cx,0FF00h ;Сравниваем чтобы не пойти на себя
	je buff_en
	mov cx,0100h
	jmp en
up:	
	cmp ah,MoveUp
	jne left
	cmp cx,0100h
	je en
	mov cx,0FF00h
	jmp en
buff_en:
	jmp en
left:
	cmp ah,MoveLeft
	jne right
	cmp cx,0001h
	je en
	mov cx,0FFFFh
	jmp en
right:
	cmp ah,MoveRight
	jne escb
	cmp cx,0FFFFh
	je en
	mov cx,0001h
    jmp en
escb:
	cmp ah,Exit
	jne en
	;Выход из программы, если была нажата кнопка esc
	mov ax,4c00h
    int 21h
en:
	ret
KeyPress endp

AddFood proc
	push ax
	push bx
	push cx
	push dx
	
sc:	
	;Функция 2Ch 21 прерывания считывает время
	mov ah,2Ch
	int 21h
	
	xor ax,ax
	mov al,dl
	mov dl,13h
	div dl 
	
	mov bl,ah ;Запись координаты
sc2:

	;Функция 2Ch 21 прерывания считывает время
	mov ah,2Ch
	int 21h

	xor ax,ax
	mov al,dl

	mov dl,0Fh
	div dl 	
	
	mov dl,bl
	mov dh,ah ;Запись координаты
	add dx,0101h
	
	xor bx,bx 
	xor cx,cx
	
	mov ax,0200h ;установить положение курсора
	int 10h	;dh -  номер строки, dl - номер столбца
	
	mov ax,0800h ;считать символ и атрибут символа в текущей позиции курсора
	int 10h	;Вывод: АН = атрибут символа, AL = ASCII-код символа.
	
	cmp al,2Ah ;Проверяем пустое ли место
	je sc
	
    cmp al,40h      
    je sc ;Если нет повторяем
	
	push cx 
	mov cx,1
	mov bl,01000001b
	;Аттрибут-ASCII-код символа(24-$)
	mov ax,0924h 
	int 10h
	pop cx
	
	pop dx
	pop cx
	pop bx
	pop ax
	
	ret
AddFood endp

end	start           