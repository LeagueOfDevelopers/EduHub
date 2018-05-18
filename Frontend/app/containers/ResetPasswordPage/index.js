/**
 *
 * ResetPasswordPage
 *
 */

import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';
import { compose } from 'redux';
import injectSaga from 'utils/injectSaga';
import injectReducer from 'utils/injectReducer';
import reducer from './reducer';
import saga from './saga';
import { resetPassword } from "./actions";
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

export class ResetPasswordPage extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props){
    super(props);

    this.state = {
      password: ''
    };

    this.onHandlePasswordChange = this.onHandlePasswordChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.goBack = this.goBack.bind(this);
  }

  goBack = () => {
    history.back()
  };

  onHandlePasswordChange = (e) => {
    this.setState({password: e.target.value})
  };

  handleSubmit = (e) => {
    e.preventDefault();
    this.props.form.validateFields((err, value) => {
      if(!err) {
        // console.log(value.password, this.props.match.params.key)
        this.props.resetPassword(value.password, this.props.match.params.key)
      }
    })
  };

  render() {
    const {getFieldDecorator} = this.props.form;
    return (
      <div style={{height: 'calc(100% - 64px)'}}>
        <Row style={{textAlign: 'center', marginTop: 40}}><h3 style={{marginBottom: 0}}>Восстановление пароля</h3></Row>
        <Row><Divider/></Row>
        <Row style={{marginTop: 40}} type='flex' justify='center'>
          <Form className='form' style={{maxWidth: 400}} onSubmit={this.handleSubmit}>
            <Col>
              <FormItem
                {...formItemLayout}
                label="Придумайте новый пароль"
              >
                {getFieldDecorator('password', {
                  rules: [
                    {required: true, message: 'Пожалуйста, введите свой пароль!'},
                    {message: 'Пароль должен быть не меньше 8 символов!', min: 8},
                    {message: 'Пароль должен быть не больше 50 символов!', max: 50}
                  ],
                  initialValue: this.state.password
                })(
                  <Input onChange={this.onHandlePasswordChange} type='password' placeholder="Введите пароль"/>)
                }
              </FormItem>
            </Col>
            <Col style={{marginTop: 20, textAlign: 'center'}}>
              <FormItem>
                <div>
                  <Button htmlType="button" style={{marginRight: '2%'}} onClick={this.goBack}>Отменить</Button>
                  <Button type="primary" htmlType="submit">Изменить пароль</Button>
                </div>
              </FormItem>
            </Col>
          </Form>
        </Row>
      </div>
    );
  }
}

ResetPasswordPage.propTypes = {
  dispatch: PropTypes.func,
};

const mapStateToProps = createStructuredSelector({

});

function mapDispatchToProps(dispatch) {
  return {
    resetPassword: (newPassword, key) => dispatch(resetPassword(newPassword, key)),
  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

const withReducer = injectReducer({ key: 'resetPasswordPage', reducer });
const withSaga = injectSaga({ key: 'resetPasswordPage', saga });

export default compose(
  withReducer,
  withSaga,
  withConnect,
)(Form.create()(ResetPasswordPage));
