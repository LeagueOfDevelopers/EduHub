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
import {Row, Col, Button} from 'antd';

export class RegistrationAccepted extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);
  }

  componentDidMount = () => {
    this.props.acceptUser(this.props.match.params.key)
  };

  render() {
    return (
      <Row style={{marginTop: 80, padding: 40, backgroundColor: 'rgba(0,0,0,0.1)'}}>
        <Col span={24} style={{marginBottom: 30, textAlign: 'center'}}>
          {this.props.status !== 400 ?
            'Вы успешно зарегистрированы.'
            :
            'Ссылка недействительна.'
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
