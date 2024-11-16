import random
import re

from yandexgptlite import YandexGPTLite

TOKEN = "y0_AgAAAAB2JgguAATuwQAAAAEYqgSXAADPzMugezFBpI9CWj6nNLyUys2UZQ"
CATALOG_ID = "b1g70eb5dcf0ifd26hjc"

account = YandexGPTLite(CATALOG_ID, TOKEN)
arr = [
    "Шут",
    "Маг",
    "Верховная Жрица",
    "Императрица",
    "Император",
    "Жрец",
    "Влюбленные",
    "Колесница",
    "Сила",
    "Отшельник",
    "Колесо Фортуны",
    "Справедливость",
    "Повешенный",
    "Смерть",
    "Умеренность",
    "Дьявол",
    "Башня",
    "Звезда",
    "Луна",
    "Солнце",
    "Суд",
    "Мир"
]
arr = list(map(lambda i: i.lower(), arr))

PROMPT_SOFT = ("Первый пользователь имеет следующие софт-скилы: {{TAG1}}. А второй имеет "
               "{{TAG2}}. Напиши какая совместная аркана им подходит ТОЛЬКО ОДНИМ СЛОВОМ из предложенных аркан. Через пробел напиши процент совместимости этих пользователей по софт скилам только числом.")
SYSTEM_PROMPT_SOFT = f"Арканы: {','.join(arr)}"


PROMPT_COMPARE_CARDS_PERCENT = "Скажи процентную совместимость двух таро карт: {{CARD1}} и {{CARD2}}"
PROMPT_COMPARE_CARDS_TEXT = "Опиши одной фразой рекомендации к взаимодействию людей, которые имеют таро карты: {{CARD1}} и {{CARD2}}."


def get_soft(tag1: str, tag2: str) -> tuple[str, int]:
    res = account.create_completion(
        PROMPT_SOFT.replace("{{TAG1}}", tag1).replace("{{TAG2}}", tag2),
        '0',
        SYSTEM_PROMPT_SOFT
    )

    new_arr = []
    for i in arr:
        if i.lower() in res.lower():
            new_arr.append(i)

    new_arr.append(random.choice(arr))

    return (new_arr[0], extract_or_random(res))


def get_hard(tags1: list[str], tags2: list[str]):
    summ = 0
    for i in tags1:
        for j in tags2:
            if i.lower() in j.lower():
                summ += 1

    return summ / max(len(tags1), len(tags2))


def get_magic_compare(first_card_name: str, second_card_name: str) -> tuple[int, str]:
    # Процент совместимость
    # Результат текстом

    percent = account.create_completion(
        PROMPT_COMPARE_CARDS_PERCENT.replace("{{CARD1}}", first_card_name).replace("{{CARD2}}", second_card_name),
        '0'
    )
    text = account.create_completion(
        PROMPT_COMPARE_CARDS_TEXT.replace("{{CARD1}}", first_card_name).replace("{{CARD2}}", second_card_name),
        "0"
    )

    return (extract_or_random(percent), text)


def extract_or_random(text) -> int:
    match = re.search(r'\d+', text)
    if match:
        return int(match.group())  # Возвращаем найденное число
    else:
        return random.randint(0, 100)  # Возвращаем случайное число