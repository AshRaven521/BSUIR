model small
.stack 100h
.data 
a dw ?
b dw ?
c dw ?
d dw ?
endl db 0dh, 0ah, '$'
num db 10 dup('$')
.code 

main:
mov ax,@data 
mov ds,ax

; DOS функция вывода строки на экран
;mov ah, 09h
;int 21h

;<readABCD>
;first IF

;a^c
mov ax,a
xor ax,c
;resuilt in ax

;b|d
mov bx,b
or bx,d
;result in bx

cmp ax,bx
;ZF==0
jnz next
;ZF==1
;print (a&b)+(c+d)
mov ax,a
and ax,b
mov bx,c
add bx,d
add ax,bx
jmp fin

;first else
next:
;second IF

;a&b
mov ax,a
and ax,b

;a+c
mov bx,a
add bx,c

;compare
cmp ax,bx
;ZF==0
jnz last
;Zf==1
;print a|b|c|d
mov ax,a
or ax,b
or ax,c
or ax,d
jmp fin

;second else
last:
;print a+b+c+d
mov ax,a
add ax,b
add ax,c
add ax,d
jmp fin



fin:
;<print>
;clear and exit
xor al,al
mov ah,4ch
int 21h
 
end main 
