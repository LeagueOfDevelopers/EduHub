/**
 *
 * RegistrationAccepted
 *
 */

import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';
import { compose } from 'redux';
import injectSaga from 'utils/injectSaga';
import injectReducer from 'utils/injectReducer';
import {makeSelectStatus} from './selectors';
import {acceptUser} from "./actions";
import reducer from './reducer';
import saga from './saga';
import {parseJwt} from "../../globalJS";
import {Row, Col, Button, Icon} from 'antd';

export class RegistrationAccepted extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);
  }

  componentDidMount = () => {
    if(localStorage.getItem('token') && parseJwt(localStorage.getItem('token')).exp - parseInt(Date.now()/1000) < 0) {
      localStorage.setItem('name', '');
      localStorage.setItem('avatarLink', '');
      localStorage.setItem('token', '');
    }
    this.props.acceptUser(this.props.match.params.key)
  };

  render() {
    return (
      <Row style={{marginTop: 80, padding: 40, fontSize: 24}}>
        <Col span={24} style={{marginBottom: 30, textAlign: 'center'}}>
          {this.props.status === 200 ?
            'Вы успешно подтвердили свой email!'
            : this.props.status === 400 ?
            'Ссылка недействительна!'
              : <Icon type="loading" />
          }
        </Col>
        <Col span={24} style={{display: 'flex', justifyContent: 'center'}}>
          <Button size='large' onClick={() => location.assign('/')}>Вернуться на главную</Button>
        </Col>
      </Row>
    );
  }
}

RegistrationAccepted.propTypes = {
  dispatch: PropTypes.func,
};

const mapStateToProps = createStructuredSelector({
  status: makeSelectStatus()
});

function mapDispatchToProps(dispatch) {
  return {
    acceptUser: (key) => dispatch(acceptUser(key)),
  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

const withReducer = injectReducer({ key: 'registrationAccepted', reducer });
const withSaga = injectSaga({ key: 'registrationAccepted', saga });

export default compose(
  withReducer,
  withSaga,
  withConnect,
)(RegistrationAccepted);
