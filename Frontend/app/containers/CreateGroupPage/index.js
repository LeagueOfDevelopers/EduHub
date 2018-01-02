/**
 *
 * CreateGroupPage
 *
 */

import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';
import { compose } from 'redux';

import injectSaga from 'utils/injectSaga';
import injectReducer from 'utils/injectReducer';
import makeSelectCreateGroupPage from './selectors';
import reducer from './reducer';
import saga from './saga';
import styled from 'styled-components';
import { Form, Col, Row, Button, Divider, message } from 'antd';
const FormItem = Form.Item;

import Header from 'components/Header';
import CreateGroupForm from 'components/CreateGroupForm';


const tailFormItemLayout = {
  wrapperCol: {
    xs: {
      span: 24,
      offset: 0,
    },
    sm: {
      span: 24,
      offset: 0,
    },
  }
};

export class CreateGroupPage extends React.PureComponent { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.createGroup = this.createGroup.bind(this);
    this.goBack = this.goBack.bind(this);
  }

  createGroup() {
    message.error('Невозможно создать группу!')
  }

  goBack() {
    history.back()
  }

  render() {
    return (
      <div>
        <header>
          <Header/>
        </header>
        <div>
          <Row style={{textAlign: 'center', marginTop: 30}}><h3>Создание группы</h3></Row>
          <Row><Divider/></Row>
          <Row style={{marginTop: 20}}><CreateGroupForm/></Row>
          <Row style={{marginTop: 20, textAlign: 'center'}}>
            <FormItem {...tailFormItemLayout}>
              <Button htmlType="button" style={{marginRight: '2%'}} onClick={this.goBack}>Отменить</Button>
              <Button type="primary" htmlType="submit" onClick={this.createGroup}>Создать группу</Button>
            </FormItem>
          </Row>
        </div>
      </div>

    );
  }
}

CreateGroupPage.propTypes = {
  dispatch: PropTypes.func.isRequired,
  createGroup: PropTypes.func.isRequired,
  goBack: PropTypes.func.isRequired
};

const mapStateToProps = createStructuredSelector({
  creategrouppage: makeSelectCreateGroupPage(),
});

function mapDispatchToProps(dispatch) {
  return {
    dispatch,
  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

const withReducer = injectReducer({ key: 'createGroupPage', reducer });
const withSaga = injectSaga({ key: 'createGroupPage', saga });

export default compose(
  withReducer,
  withSaga,
  withConnect,
)(CreateGroupPage);
