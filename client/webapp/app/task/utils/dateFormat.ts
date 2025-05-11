export function formatDate(dateString: string): string {
  const date = new Date(dateString);

  const year = date.getFullYear().toString().slice(-2); // последние две цифры года
  const month = String(date.getMonth() + 1).padStart(2, "0"); // месяцы от 01 до 12
  const day = String(date.getDate()).padStart(2, "0"); // дни от 01 до 31
  const hours = String(date.getHours()).padStart(2, "0"); // часы от 00 до 23
  const minutes = String(date.getMinutes()).padStart(2, "0"); // минуты от 00 до 59

  // Формируем строку в требуемом формате
  return `${year}/${month}/${day} ${hours}:${minutes}`;
}

export function formatDateTimeForInput(date: Date): string {
  // Отримуємо рік, місяць, день, години, хвилини та секунди
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, "0");
  const day = String(date.getDate()).padStart(2, "0");
  const hours = String(date.getHours()).padStart(2, "0");
  const minutes = String(date.getMinutes()).padStart(2, "0");
  const seconds = String(date.getSeconds()).padStart(2, "0");

  // Форматуємо дату та час для input
  return `${year}-${month}-${day}T${hours}:${minutes}:${seconds}`;
}
