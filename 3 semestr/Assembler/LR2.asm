model small
.stack 100h
.data 
    a dw ?
    b dw ?
    c dw ?
    d dw ?
    endl db 0dh, 0ah, '$'
    num db 10 dup('$')
    errormes db 'Bad input$'
    backmes db "Backspace$"
    zapros db "Input numbers: $"
.code

main:
    mov ax,@data    
    mov ds,ax

    xor dx,dx
    
    ;ввод чисел с клавиатуры с помощью  процедуры
    call InputInt
    cmp bx,0
    jz exitout
    ;после вызова процедуры значение с клавиатуры хранится в ax, поэтому переносим в нужную нам переменную
    mov a,bx
    ;обнуление регистра dx для дальнейшего его использования
    xor dx,dx

    call InputInt
    cmp bx,0
    jz exitout
    mov b,bx
    xor dx,dx

    call InputInt
    cmp bx,0
    jz exitout
    mov c,bx
    xor dx,dx

    call InputInt
    cmp bx,0
    jz exitout
    mov d,bx

    call ProblemSolving

exitout:
    ;clear and exit
    xor al, al
    mov ah,4ch
    int 21h



InputInt proc 

    ;mov dx,offset zapros
    ;mov ah,9
    ;int 21h

    xor bx,bx
nextnum:
 
    ;вводим новый символ
    mov ah,01h 
    int 21h

    ; если нажали enter то это конец числа
    ;cmp al,2fh   
    ;jl outp
    ; если введен неверный символ <0
    cmp al,0Dh
    jz outp
    cmp al,0Ah
    jz outp
    cmp al,'0' 
    jb er
    ; если введен неверный символ >9
    cmp al,'9'
    ja er
    
   
    ;делаем из введенного символа число   
    sub al,'0'
    xor ah,ah
    xchg ax,bx
    mov dx,0Ah
    ; умножаем на основание системы счисления = 10  
    mul dx  
    
    ;и прибавляем новое = сумма
    add bx,ax

    ;cmp bx,1
    ;jb er
    ;cmp bx,5000
    ;jae er
    jmp nextnum

er:
   
    ; caret   db  0Ah,0Dh,'$'   
    mov dx,offset endl
    mov ah,9
    int 21h
 
    ; error   db  'Symbol not correct!$' 
    mov dx,offset errormes
    mov ah,9
    int 21h

    mov dx,offset endl
    mov ah,9
    int 21h

    xor bx,bx

outp:
    mov ax,bx   
    ret
InputInt endp

ProblemSolving proc
    ;first IF

    ;a|c
    mov ax,a
    or ax,c
    ;result in AX

    ;b^d
    mov bx,b
    xor bx,d
    ;result in BX

    ;compare AX and BX
    cmp ax,bx
    ;
    ;ZF==1
    jnz next
    ;ZF==0
    mov ax,a
    xor ax,b
    xor ax,c
    add ax,d
    jmp final

    ;first else

next:
    ;second IF

    ;a+b
    mov ax,a
    add ax,b
    ;c^d
    mov bx,c
    xor bx,d
    ;compare
    cmp ax,bx
    ;ZF==0
    jnz last
    ;ZF==1
    mov ax,a
    and ax,d
    mov bx,b
    add bx,d
    or ax,bx
    jmp final

    ;second else
last:
    mov ax,b
    add ax,c
    xor ax,a
    or ax,d
    jmp final
    ;

final:
    ;mov a,ax
    ;mov ah,09h
    ;mov dx,offset endl
    ;int 21h
    ;mov ax,a
    call NumberOutput

    ret
ProblemSolving endp 

NumberOutput proc
    xor     cx, cx
    ; основание сс. 10 для десятеричной и т.п.
    mov     bx, 10 
oi2:
    xor     dx,dx
    div     bx
    ; Делим число на основание сс. В остатке получается последняя цифра.
    ; Сразу выводить её нельзя, поэтому сохраним её в стэке.
    push    dx
    inc     cx
    ; А с частным повторяем то же самое, отделяя от него очередную
    ; цифру справа, пока не останется ноль, что значит, что дальше
    ; слева только нули.
    test    ax, ax
    jnz     oi2
    ; Теперь приступим к выводу.
    mov     ah, 02h
oi3:
    pop     dx
    add     dl, '0'
    int     21h
    loop    oi3
    
    ret
; конец
NumberOutput endp


;конец программы с точкой входа main
end main