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
  }

  body.fontLoaded {
    font-family: 'Open Sans', 'Helvetica Neue', Helvetica, Arial, sans-serif;
  }

  #app {
    background-color: #fff;
    min-height: 100%;
    min-width: 100%;
  }

  p,
  label {
    font-family: Tahoma, Helvetica, Arial, sans-serif;
    line-height: 1.5em;
  }
`;
