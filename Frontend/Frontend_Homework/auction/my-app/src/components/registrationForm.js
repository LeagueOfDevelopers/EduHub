import React from 'react';
import SuccessBtn from './success-btn';
import DangerBtn from './danger-btn';

export default function RegistrationForm(){
  return (
      <div class="uk-card uk-card-large uk-card-default">
        <div className="head-item"><h3>Creating account</h3></div>
        <div className="uk-margin">
          <input className="uk-input" type="text" placeholder="Enter your login"/>
          <input className="uk-input" type="password" placeholder="Enter your password"/>
          <input className="uk-input" type="password" placeholder="Repeat your password"/>
        </div>
        <div styles="display: inline-block;">
          <SuccessBtn title="confirm"/>
          <DangerBtn title="cancel"/>
        </div>
      </div>
  );
};