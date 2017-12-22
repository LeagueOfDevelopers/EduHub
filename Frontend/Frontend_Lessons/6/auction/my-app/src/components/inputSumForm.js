import React from 'react';
import SuccessBtn from './success-btn';
import DangerBtn from './danger-btn';

export default function InputSumForm(){
  return (
      <div class="uk-card uk-card-large uk-card-default">
        <h3>Placing a bet</h3>
        <div className="uk-margin">
          <input className="uk-input" type="text" placeholder="Enter your sum"/>
        </div>
        <div styles="display: inline-block;">
          <SuccessBtn title="confirm"/>
          <DangerBtn title="cancel"/>
        </div>
      </div>
      );
};