from tkinter import *
from tkinter import messagebox
from lib import start


# Проверяем введенные данные в input
# def callback(input):
#     if input.isdigit():
#         print(input)
#         return True
#
#     elif input is "":
#         # print(input)
#         messagebox.showwarning(title="Предупреждение", message="Вы ничего не ввели в поле ввода!")
#         return False
#
#     else:
#         # print(input)
#         messagebox.showwarning(title="Предупреждение", message="Введите число!")
#         return False


def calculate_characteristics(event):
    # Количество каналов в СМО
    channels = int(queuing_system_channels_text.get())
    # Размер очереди(мест в очереди)
    queue_size = int(queue_size_text.get())
    # Интенсивность потока заявок(заявок/час)
    request_intensity = int(request_intensity_text.get())
    # Интенсивность потока обслуживания(заявок/час)
    service_intensity = int(service_intensity_text.get())
    # Вероятность обработки запроса
    prob_to_serve = float(probability_to_handle_request_text.get())

    # time_interval_for_info = 2

    run_life_time = 1000

    start(
        channels,
        queue_size,
        request_intensity,
        service_intensity,
        prob_to_serve,
        run_life_time
    )


window = Tk()
window.title("Lr4")
window.geometry("300x400")
window.resizable(False, False)

main_label = Label(window, text="Характеристики СМО", font=('Helvetica bold', 15))
main_label.place(x=50, y=0)

queuing_system_channels_label = Label(window, text="Введите количество каналов в СМО")
queuing_system_channels_label.place(x=10, y=36)
queuing_system_channels_text = Entry(window, bd=5)
queuing_system_channels_text.place(x=42, y=68)

queue_size_label = Label(window, text="Введите количество мест в очереди")
queue_size_label.place(x=10, y=98)
queue_size_text = Entry(window, bd=5)
queue_size_text.place(x=42, y=128)

request_intensity_label = Label(window, text="Введите интенсивность поступления заявок в час")
request_intensity_label.place(x=10, y=158)
request_intensity_text = Entry(window, bd=5)
request_intensity_text.place(x=42, y=188)

service_intensity_label = Label(window, text="Введите интенсивность обработки заявок в час")
service_intensity_label.place(x=10, y=218)
service_intensity_text = Entry(window, bd=5)
service_intensity_text.place(x=42, y=248)

probability_to_handle_request_label = Label(window, text="Введите вероятность обработки запроса")
probability_to_handle_request_label.place(x=10, y=278)
probability_to_handle_request_text = Entry(window, bd=5)
probability_to_handle_request_text.place(x=42, y=308)

# Регистрируем функцию проверки данных в input
# reg = window.register(callback)
# На каждый input добавляем функцию проверки данных
# queuing_system_channels_text.config(validate="key",
#                                     validatecommand=(reg, '% P'))
# queue_size_text.config(validate="key",
#                        validatecommand=(reg, '% P'))
#
# request_intensity_text.config(validate="key",
#                               validatecommand=(reg, '% P'))
#
# service_intensity_text.config(validate="key",
#                               validatecommand=(reg, '% P'))
#
# probability_to_handle_request_text.config(validate="key",
#                                           validatecommand=(reg, '% P'))

calculate_characteristics_button = Button(window, text="Подсчитать характеристики")
calculate_characteristics_button.place(x=60, y=350)
calculate_characteristics_button.bind('<Button-1>', calculate_characteristics)

window.mainloop()
