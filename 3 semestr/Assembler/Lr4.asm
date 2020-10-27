model small
.stack 100h
.data
    string db 50 dup('$')
    subString db 50 dup('$')
    stringLength dw 0
    subStringLength dw 0
    arrpi db 50 dup('$')
    endl db 0dh, 0ah, '$'
    yesString db 'yes$'
    noString db 'no$'
.code
main:

    mov ax, @data
    mov ds, ax

    call Input

    call PIFunc

    call StringSearch

    mov al, 0
    mov ah, 4ch
    int 21h

Input proc
    lea si, subString
 
 ;чтение подстроки, т.к ее вводят первой   
ReadSubstring:

    mov ah, 01h
    int 21h
    ;проверка на наличие пробела, если таковой имеется, т.е ZF=1 и осуществится переход по метке для прекращения чтения подстроки
    cmp al, ' '
    jz StopReadSubstring
    mov [si], al
    inc si
    inc subStringLength
    jmp ReadSubstring

StopReadSubstring:

    ;в si записываем адерес строки string
    lea si, string

;чтение строки
ReadString:

    mov ah, 01h
    int 21h
    cmp al, 0dh
    jz StopReadString
    cmp al, 0ah
    jz StopReadString
    ;команда [] позволяет записать в напрямую в адрес значение, а не в регистр
    mov [si], al
    inc si
    inc stringLength
    jmp ReadString

StopReadString:

    mov stringLength, si

Final:
    ret
Input endp

StringSearch proc

    ;обнулим регистры для дальнейшего использования
    mov si, 0
    mov bx, 0

Match:

    ;проверим не закончилась ли строка
    cmp si, stringLength
    ;переход, если ZF=1(в данном случае подстрока пустая)
    jz NotFind

    mov al, string[si]
    cmp al, subString[bx]
    ;переход, если ZF=0
    jnz NotMatch
    ;увеличиваем значения регистров
    inc si
    inc bx

    cmp bx, subStringLength
    ;если значения bx == subStringLength, то осуществится переход для вывода "yes", т.к в строке было найдено нужное кол-во символов 
    jz Find
    jmp Match

NotMatch:

    cmp bx, 0
    ;проверка на наличие символов в строке
    jnz NotNull
    inc si
    jmp Match

NotNull:

    mov bl, arrpi[bx - 1]
    jmp Match
    
Find:

    ;выводим сначала перенос строки, а потом строку "yes"
    mov ah, 09h
    mov dx, offset endl
    int 21h
    mov dx, offset yesString
    int 21h
    jmp Stop
   
NotFind:

    ;выводим сначала перенос строки, а потом строку "no"
    mov ah, 09h
    mov dx, offset endl
    int 21h
    mov dx, offset noString
    int 21h

Stop:
    ret
StringSearch endp

PIFunc proc
    xor ax, ax
    ;в cx будем хранить длину подстроки
    mov cx, subStringLength

    mov si, 1
    mov bx, 0

    ;заносим в первый элемент массива 0
    ;массив pi содержит значение, которое покажет на сколько мы можем сдвинуть подстроку вдоль строки
    ;с помощью этого массива, при сдвиге подстроки индекс строки никогда не возвращается назад
    mov arrpi[0], 0

FillingPiArray:

    cmp cx, 1
    jz Finish
    ;сравниваем второй символ подстроки с первым
    mov al, subString[si]
    cmp al, subString[bx]
    ;переходим по метке, если не равно нулю, т.е первый и второй символ не совпадают
    jnz NotEqual

    ;увеличив bx и si на 1, сдвинемся на один символ вправо в подстроке
    inc bx
    mov arrpi[si], bl
    inc si
    dec cx
    ;и повторим итерацию
    jmp FillingPiArray

NotEqual:

    cmp bx, 0
    jz EqualZero
    ;если индекс, указывающий на подстроку не равен нулю, то приравниваем индекс, указывающий на подстроку к pi[j-1](т.е обращаемся к массиву pi)
    mov bl, arrpi[bx - 1]
    jmp FillingPiArray

EqualZero:

    ;если индекс, указывающий на подстроку равен нулю, то увеличиваем индекс, указывающий на строку
    mov arrpi[si], 0
    inc si
    dec cx
    jmp FillingPiArray

Finish:
    ret
PIFunc endp

;конец программы с точкой входа main
end main