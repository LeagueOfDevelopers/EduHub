/**
 *
 * RegistrationSuccessPage
 *
 */

import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { compose } from 'redux';
import {Row, Col, Button} from 'antd';

export class RegistrationSuccessPage extends React.PureComponent { // eslint-disable-line react/prefer-stateless-function
  render() {
    return (
      <Row style={{marginTop: 80, padding: 40, backgroundColor: 'rgba(0,0,0,0.1)'}}>
        <Col span={24}>
          <div style={{textAlign: 'center'}}>
            Заявка на регистрацию принята.
          </div>
          <div style={{textAlign: 'center'}}>
            Дальнейшие инструкции высланы вам на почту.
          </div>
        </Col>
      </Row>
    );
  }
}

RegistrationSuccessPage.propTypes = {
  dispatch: PropTypes.func.isRequired,
};


function mapDispatchToProps(dispatch) {
  return {
    dispatch,
  };
}

const withConnect = connect(null, mapDispatchToProps);

export default compose(
  withConnect,
)(RegistrationSuccessPage);
