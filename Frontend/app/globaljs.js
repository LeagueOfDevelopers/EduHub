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
      return 'Стандартная';
    case 1:
      return 'Лекция';
    case 2:
      return 'Семинар';
    case 3:
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


export function getQueryVariable(variable) {
  let query = window.location.search.substring(1);
  let vars = query.split('&');
  for (let i = 0; i < vars.length; i++) {
    let pair = vars[i].split('=');
    if (decodeURIComponent(pair[0]) == variable) {
      return decodeURIComponent(pair[1]);
    }
  }
}
