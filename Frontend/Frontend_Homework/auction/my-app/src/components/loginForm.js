import React from 'react';
import SuccessBtn from './success-btn';
import DangerBtn from './danger-btn';

export default function LoginForm(){
  return (
      <div class="uk-card uk-card-large uk-card-default">
        <div className="head-item"><h3>Login In</h3></div>
        <div className="inputs">
          <input className="uk-input" type="text" placeholder="Enter your mail"/>
          <input className="uk-input" type="text" placeholder="Enter your password"/>
        </div>
        <button className="uk-button uk-button-text forgot-password">Forgot password?</button>
        <div styles="display: inline-block;">
          <SuccessBtn title="confirm"/>
          <DangerBtn title="cancel"/>
        </div>
      </div>
  );
};