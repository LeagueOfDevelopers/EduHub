import React, { Component } from 'react'; 

import icon from '../images/logo.png';

export default class SearchBar extends Component{
  render(){
    return (
        <nav className="uk-navbar-container" >
          {/*<div className="uk-navbar-left">*/}

            <div className="uk-navbar-item">
              <form className="uk-search uk-search-navbar">
                <a className="uk-search-icon uk-search-icon-flip">
                  <img src={require('../images/logo.svg')} alt=""/>
                </a>
                <input className="uk-search-input" type="search" placeholder="Search..." />
              </form>
            </div>

          {/*</div>*/}
        </nav>
    );
  }
}


