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
      return 'Преподаватель';
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

export function getNotifySettingType(enumType) {
  switch (enumType) {
    case 0:
      return 'Default';
    case 1:
      return 'DontSend';
    case 2:
      return 'ToMail';
    case 3:
      return 'OnSite';
    case 4:
      return 'Everywhere';
  }
}

export function getNotifySettingTypeRevert(enumType) {
  switch (enumType) {
    case 'Default':
      return 0;
    case 'DontSend':
      return 1;
    case 'ToMail':
      return 2;
    case 'OnSite':
      return 3;
    case 'Everywhere':
      return 4;
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

export function getGenderType(gender) {
  switch (gender) {
    case 'Неизвестно':
      return 0;
    case 'Мужской':
      return 1;
    case 'Женский':
      return 2;
    case 'Unknown':
      return 0;
    case 'Man':
      return 1;
    case 'Woman':
      return 2;
  }
}

export function getCourseStatus(enumType) {
  switch (enumType) {
    case 0:
      return 'В ожидании';
    case 1:
      return 'Идет одобрение плана обучения';
    case 2:
      return 'Курс начат';
    case 3:
      return 'Курс завершен';
  }
}

export function getSanctionType(enumType) {
  switch (enumType) {
    case 0:
      return 'Не разрешено преподавать';
    case 1:
      return 'Не разрешено вступать в группы';
    case 2:
      return 'Не разрешено редактировать профиль';
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

export function connectSockets(groupId) {
  const uri = `ws://localhost:10485/api/group/{groupId}/chat`;
  let socket = new WebSocket(uri);
  socket.onopen = function(event) {
    console.log("opened connection to " + uri);
  };
  socket.onclose = function(event) {
    console.log("closed connection from " + uri);
  };
  socket.onmessage = function(event) {
    console.log('message received' + JSON.stringify(event.data));
  };
  socket.onerror = function(event) {
    console.log("error: " + JSON.stringify(event));
  };
}
