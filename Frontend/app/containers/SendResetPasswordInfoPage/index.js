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
import { sendResetPasswordInfo } from "./actions";
import injectSaga from 'utils/injectSaga';
import injectReducer from 'utils/injectReducer';
import reducer from './reducer';
import saga from './saga';
import { Form, Col, Row, Button, Divider, message, Input, Switch } from 'antd';
const FormItem = Form.Item;

const formItemLayout = {
  labelCol: {
    xs: { span: 24 }
  },
  wrapperCol: {
    xs: { span: 24 }
  },
};

export class SendResetPasswordInfoPage extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props){
    super(props);

    this.state = {
      email: ''
    };

    this.onHandleEmailChange = this.onHandleEmailChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.goBack = this.goBack.bind(this);
  }

  goBack = () => {
    history.back()
  };

  onHandleEmailChange = (e) => {
    this.setState({email: e.target.value})
  };

  handleSubmit = (e) => {
    e.preventDefault();
    this.props.form.validateFields((err, value) => {
      if(!err) {
        this.props.sendResetPasswordInfo(value.email)
      }
    })
  };

  render() {
    const {getFieldDecorator} = this.props.form;
    return (
      <div>
        <Row style={{textAlign: 'center', marginTop: 40}}><h3 style={{marginBottom: 0}}>Восстановление пароля</h3></Row>
        <Row><Divider/></Row>
        <Row style={{marginTop: 40}} type='flex' justify='center'>
          <Form className='form' style={{width: 400}} onSubmit={this.handleSubmit}>
            <Col>
              <FormItem
                {...formItemLayout}
                label="Введите свой email"
              >
                {getFieldDecorator('email', {
                  rules: [
                    {required: true, message: 'Пожалуйста, введите свой email!'}
                  ],
                  initialValue: this.state.email
                })(
                  <Input onChange={this.onHandleEmailChange} placeholder="Введите email"/>)
                }
              </FormItem>
            </Col>
            <Col style={{marginTop: 20, textAlign: 'center'}}>
              <FormItem>
                <div>
                  <Button htmlType="button" style={{marginRight: '2%'}} onClick={this.goBack}>Отменить</Button>
                  <Button type="primary" htmlType="submit">Подтвердить</Button>
                </div>
              </FormItem>
            </Col>
          </Form>
        </Row>
      </div>
    );
  }
}

SendResetPasswordInfoPage.propTypes = {
  dispatch: PropTypes.func,
};

const mapStateToProps = createStructuredSelector({

});

function mapDispatchToProps(dispatch) {
  return {
    sendResetPasswordInfo: (email) => dispatch(sendResetPasswordInfo(email)),
  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

const withReducer = injectReducer({ key: 'sendResetPasswordInfoPage', reducer });
const withSaga = injectSaga({ key: 'sendResetPasswordInfoPage', saga });

export default compose(
  withReducer,
  withSaga,
  withConnect,
)(Form.create()(SendResetPasswordInfoPage));
