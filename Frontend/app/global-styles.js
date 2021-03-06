import { injectGlobal } from 'styled-components';

/* eslint no-unused-expressions: 0 */
injectGlobal`
  html {
    font-size: 10px;
  }

  html,
  body {
    height: 100%;
    width: 100%;
    margin: 0;
    padding: 0;
  }

  body {
    font-family: Tahoma, Helvetica, Arial, sans-serif;
    font-size: 18px;
    overflow-y: scroll;
  }

  body.fontLoaded {
    font-family: 'Open Sans', 'Helvetica Neue', Helvetica, Arial, sans-serif;
  }

  #app {
    background-color: #ffffff;
    min-height: 100%;
    min-width: 100%;
  }
  
  #footer {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 64px;
    width: 100%;
    margin-top: 40px;
    font-size: 16px;
    padding: 10px;
    background-color: #2C365D;
    color: #ffffff;
  }

  p,
  label {
    font-family: Tahoma, Helvetica, Arial, sans-serif;
    line-height: 1.5em;
  }
`;
