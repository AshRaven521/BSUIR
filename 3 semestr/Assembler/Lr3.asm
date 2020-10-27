model small
.stack 100h
.data
    a dw ?
    b dw ?
    c dw ?
    d dw ?
    delimoe dw ?
    delitel dw ?
    result dw ?
    ostatok dw ?
    minus db ?
    endl db 0dh, 0ah, '$'
    er db 'Bad input$'
    zerodiv db 'Zero division$'
.code
main:
    mov ax, @data
    mov ds, ax

    call InputProc
    mov a, ax

    call InputProc
    mov b, ax

    call InputProc
    mov c, ax

    call InputProc
    mov d, ax

    mov ax, a
    cmp ax, b
    jl Internal
    jz Internal

    mov ax, c
    sub ax, d
    add ax, a 

    call OutputProc
    jmp Finish

ZeroDivision:
    mov ah, 09h
    mov dx, offset zerodiv
    int 21h
    jmp Finish

;внутренний if
Internal:
    cmp d, 0
    jz ZeroDivision
    mov cx, a
    sub cx, b
    mov ax, c
    mov bx, d
    call DivisionProc
    mov ax, result
    cmp cx, ax
    jg InternalElse
    jz InternalElse

    cmp a, 0
    jz ZeroDivision
    mov ax, c
    mov bx, a
    call DivisionProc
    mov dx, ostatok
    mov ax, b
    sub ax, dx

    call OutputProc
    jmp Finish

;внутренний else
InternalElse:
    mov ax, c
    sub ax, a
    add ax, d

    call OutputProc
;метка выхода из программы
Finish:
    mov al, 0
    mov ah, 4ch
    int 21h

OutputProc proc
	;test по сути та же инструкция, что и and, за исключением того, что она влияет только на флаги, но не сохраняет никуда результат
    test ax, ax
    jns Skip
    mov bx, ax
    neg bx
    mov ah, 02h
    mov dl, '-'
    int 21h
    mov ax, bx
;метка для пропуска знака "-"    
Skip:
    mov bx, 10
    mov cx, 0

;пушим остаток от деления числа на 10(для вывода на экран)
DigitsOut:
    mov dx, 0
    div bx
    push dx
    inc cx
    cmp ax, 0
    jnz DigitsOut    

    mov ah, 02h
;вывод остатка числа из стека    
print:
    pop dx
    add dl, '0'
    int 21h
    loop print

    ret
OutputProc endp

InputProc proc
    mov bx, 10
    xor dx, dx

    mov ah, 01h
    int 21h
    cmp al, '-'
    jnz Digits
    mov minus, '-'

DigitsIn:
    mov ah, 01h
    int 21h
    cmp al, 0dh
    jz Fin
    cmp al, 0ah
    jz Fin
Digits:
    cmp al, '0'
    ;переходим на метку ошибки, если введена не цифра
    jb InputEr
    cmp al, '9'
    ;переходим на метку ошибки, если введена не цифра
    ja InputEr
    mov cl, al
    sub cl, '0'
    mov ax, dx
    mul bx
    add ax, cx
    mov dx, ax
    jmp DigitsIn

InputEr:
    mov ah, 09h
    mov dx, offset endl
    int 21h
    mov ah, 09h
    mov dx, offset er
    int 21h
    mov al, 0
    mov ah, 4ch
    int 21h

Fin:
    mov ax, dx
    cmp minus, '-'
    jnz Exit
    neg ax
Exit:
	;если не сделать данное действие, то при последующем выводе числа программа также выведет "-" и может неправильно записать значение в регистр
    mov minus, 0
    ret
InputProc endp

; r = a - [a/b] * b
DivisionProc proc
    mov delimoe, ax
    mov delitel, bx

    ;команда cwd расширяет регистр, с которым в данный момент работаешь
    cwd
    ;idiv проводит деление со знаковыми числами
    idiv bx
    test bx, delimoe
    js Other
    ;если в результате деления чисел разных знаков 0, то делаем -1
    cmp ax, 0
    jz Nul
    test ax, ax
    jns Other
    cmp dx, 0
    jz Other
Nul:
    dec ax

;если знак делимого == знак делителя(для двух отрицательных чисел)
Other:
    imul bx
    ;для упрощения сложения [a/b]*b + a
    neg ax
    add ax, delimoe
    mov ostatok, ax
    
    mov ax, delimoe
    sub ax, ostatok
    cwd 
    idiv delitel
    mov result, ax
Last:
    ret
DivisionProc endp

;конец программы с точкой входа main
end main