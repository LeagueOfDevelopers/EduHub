/**
 *
 * ResetPasswordAcceptedPage
 *
 */

import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';
import { compose } from 'redux';

import injectSaga from 'utils/injectSaga';
import injectReducer from 'utils/injectReducer';
import makeSelectResetPasswordAcceptedPage from './selectors';
import reducer from './reducer';
import saga from './saga';
import {Row, Col, Button} from 'antd';

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

const mapStateToProps = createStructuredSelector({

});

function mapDispatchToProps(dispatch) {
  return {
    dispatch,
  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

const withReducer = injectReducer({ key: 'resetPasswordAcceptedPage', reducer });
const withSaga = injectSaga({ key: 'resetPasswordAcceptedPage', saga });

export default compose(
  withReducer,
  withSaga,
  withConnect,
)(ResetPasswordAcceptedPage);
