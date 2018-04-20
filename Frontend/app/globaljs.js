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
