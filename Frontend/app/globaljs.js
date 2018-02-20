export function parseJwt (token) {
  const base64Url = token.split('.')[1];
  const base64 = base64Url.replace('-', '+').replace('_', '/');
  return JSON.parse(window.atob(base64));
}

export function getMemberRole(enumRole) {
  switch (enumRole) {
    case 0:
      return 'Обычный пользователь';
    case 1:
      return 'Участник';
    case 2:
      return 'Создатель';
    case 3:
      return 'Учитель';
  }
}


export function getGroupType(enumType) {
  switch (enumType) {
    case 0:
      return 'Лекция';
    case 1:
      return 'Семинар';
    case 2:
      return 'Мастер-класс';
  }
}

export function getGender(enumType) {
  switch (enumType) {
    case 0:
      return 'Неизвестно';
    case 1:
      return 'Мужской';
    case 2:
      return 'Женский';
  }
}
