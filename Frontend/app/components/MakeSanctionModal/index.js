/**
 *
 * MakeSanctionModal
 *
 */

import React from 'react';
import { createStructuredSelector } from 'reselect';
import { connect } from 'react-redux';
import { searchUsers, applySanction } from "../../containers/AdminPage/actions";
import { makeSelectUsers } from "../../containers/AdminPage/selectors";
import { Form, Input, Col, Row, Modal, Button, message, Select, DatePicker } from 'antd';
const FormItem = Form.Item;


class MakeSanctionModal extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      selectValue: '',
      selectedUserName: '',
      selectedUserId: '',
      sanctionTypeValue: 'NotAllowToTeach',
      brokenRuleValue: '',
      expDateValue: ''
    };

    this.onHandleTypeChange = this.onHandleTypeChange.bind(this);
    this.onHandleTitleChange = this.onHandleTitleChange.bind(this);
    this.onHandleBrokenRuleChange = this.onHandleBrokenRuleChange.bind(this);
    this.onHandleExpDateChange = this.onHandleExpDateChange.bind(this);
    this.applySanction = this.applySanction.bind(this);
  }

  onHandleTitleChange = (e) => {
    this.setState({selectValue: e})
  };

  onHandleTypeChange = (e) => {
    this.setState({sanctionTypeValue: e})
  };

  onHandleBrokenRuleChange = (e) => {
    this.setState({brokenRuleValue: e.target.value})
  };

  onHandleExpDateChange = (value, dateString) => {
    this.setState({expDateValue: dateString})
  };

  applySanction = () => {
    this.state.expDateValue && this.state.brokenRuleValue && this.state.selectedUserId && this.state.sanctionTypeValue ?
      this.props.applySanction(this.state.selectedUserId, this.state.brokenRuleValue, this.state.sanctionTypeValue, this.state.expDateValue)
      : message.error('Введите все данные!')
  };

  render() {
    return (
      <Modal
        visible={this.props.visible}
        onCancel={this.props.handleCancel}
        width='500px'
        closable={false}
        bodyStyle={{padding: '30px 10%'}}
        footer={null}
      >
        <Row style={{margin: '10px 0 44px'}}>
          <h2 style={{marginBottom: 0}}>Выписка санкции</h2>
        </Row>
        <Form>
          <FormItem>
            <Select
              mode='combobox'
              className='unhover'
              style={{width: '100%'}}
              value={this.state.selectValue}
              onChange={this.onHandleTitleChange}
              placeholder='Выберите пользователя...'
              defaultActiveFirstOption={false}
              showArrow={false}
            >
              {this.props.users.map(item =>
                <Select.Option key={item.name}>
                  <div onClick={() => this.setState({selectedUserId: item.id, selectedUserName: item.name})}>{item.name}</div>
                </Select.Option>)
              }
            </Select>
          </FormItem>
          <FormItem>
            <Select value={this.state.sanctionTypeValue} onChange={this.onHandleTypeChange} placeholder='Тип санкции'>
              <Select.Option value="NotAllowToTeach">Запрет на преподавание</Select.Option>
              <Select.Option value="NotAllowToJoinGroup">Запрет на присоединение к учебным группам</Select.Option>
              <Select.Option value="NotAllowToEditProfile">Запрет на редактирование профиля</Select.Option>
            </Select>
          </FormItem>
          <FormItem>
            <Input.TextArea placeholder='Напишите причину' onChange={this.onHandleBrokenRuleChange} value={this.state.brokenRuleValue} autosize={{minRows: 3}}/>
          </FormItem>
          <FormItem>
            <DatePicker
              showTime
              format="DD-MM-YYYY HH:mm:ss"
              placeholder="Выберите время завершения действия санкции"
              onChange={this.onHandleExpDateChange}
              onOk={this.onHandleExpDateChange}
              style={{width: '100%'}}
            />
          </FormItem>
          <Row type='flex' justify='center' className='lg-center-container-item' style={{marginTop: 40}}>
            <Button
              className='group-btn lg-margin-right-0'
              type='primary'
              style={{width: 160, marginRight: 20, marginBottom: 10}}
              onClick={this.applySanction}
            >
              Подтвердить
            </Button>
            <Button className='group-btn' onClick={this.props.handleCancel} style={{width: 160, marginBottom: 10}}>Отменить</Button>
          </Row>
        </Form>
      </Modal>
    );
  }
}

MakeSanctionModal.propTypes = {

};

const mapStateToProps = createStructuredSelector({
  users: makeSelectUsers()
});

function mapDispatchToProps(dispatch) {
  return {
    applySanction: (userId, brokenRule, sanctionType, expirationDate) => dispatch(applySanction(brokenRule, userId, sanctionType, expirationDate)),
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(MakeSanctionModal);
