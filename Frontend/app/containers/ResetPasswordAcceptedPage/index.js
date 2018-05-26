/**
 *
 * SendResetPasswordInfoPage
 *
 */

import React from 'react';
import PropTypes from 'prop-types';
import {Row, Col} from 'antd';

export class ResetPasswordAcceptedPage extends React.Component { // eslint-disable-line react/prefer-stateless-function
  render() {
    return (
      <Row style={{marginTop: 80, padding: 40, fontSize: 24}}>
        <Col span={24}>
          <div style={{textAlign: 'center'}}>
            Заявка на изменение пароля принята.
          </div>
          <div style={{textAlign: 'center'}}>
            Дальнейшие инструкции высланы вам на почту.
          </div>
        </Col>
      </Row>
    );
  }
}

ResetPasswordAcceptedPage.propTypes = {
  dispatch: PropTypes.func,
};

export default ResetPasswordAcceptedPage;
