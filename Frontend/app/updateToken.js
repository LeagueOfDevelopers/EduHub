import {parseJwt} from "./globalJS";
import config from "./config";

export const updateToken = store => next => action => {
  if(localStorage.getItem('token') && parseJwt(localStorage.getItem('token')).exp - parseInt(Date.now()/1000) < 300) {
    fetch(`${config.API_BASE_URL}/account/refresh`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${localStorage.getItem('token')}`,
        'Content-Type': 'application/json-patch+json'
      }
    })
      .then(res => res.json())
      .then(res => {
        if(action.token !== undefined) {
          localStorage.setItem('name', `${res.name}`);
          localStorage.setItem('avatarLink', `${res.avatarLink}`);
          localStorage.setItem('token', `${res.token}`);
          localStorage.setItem('isTeacher', `${res.isTeacher}`);
          location.reload();
        }
      })
      .catch(error => error)
  }
  else {
    return next(action)
  }
};

