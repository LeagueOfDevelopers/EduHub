/**
*
* Footer
*
*/

import React from 'react';
// import styled from 'styled-components';


class Footer extends React.PureComponent { // eslint-disable-line react/prefer-stateless-function
  render() {
    return (
      <div className='footer'>
        &copy; {new Date().getFullYear()} League Of Developers
      </div>
    );
  }
}

Footer.propTypes = {

};

export default Footer;
