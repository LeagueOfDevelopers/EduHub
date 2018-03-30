/**
*
* MakeSanctionModal
*
*/

import React from 'react';
import { createStructuredSelector } from 'reselect';
import { connect } from 'react-redux';
import { Form, Input, Col, Row, Modal, Button, message, Select } from 'antd';
const FormItem = Form.Item;


class MakeSanctionModal extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {

    };

    this.onHandleReviewChange = this.onHandleReviewChange.bind(this);
    this.onHandleTitleChange = this.onHandleTitleChange.bind(this);
  }

  onHandleTitleChange = (e) => {
    this.setState({title: e.target.value})
  };

  onHandleReviewChange = (e) => {
    this.setState({review: e.target.value})
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
            <Input value={this.state.title} onChange={this.onHandleTitleChange} placeholder="Выберите пользователя..."/>
          </FormItem>
          <FormItem>
            <Select placeholder='Тип санкции'>
              <Select.Option value="1">Запрет на преподавание</Select.Option>
              <Select.Option value="2">Запрет на присоединение к учебным группам</Select.Option>
              <Select.Option value="3">Запрет на редактирование профиля</Select.Option>
            </Select>
          </FormItem>
          <Row type='flex' justify='center' className='lg-center-container-item' style={{marginTop: 40}}>
            <Button
              className='group-btn lg-margin-right-0'
              type='primary'
              style={{width: 160, marginRight: 20, marginBottom: 10}}
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

});

function mapDispatchToProps(dispatch) {
  return {

  }
}

export default connect(mapStateToProps, mapDispatchToProps)(MakeSanctionModal);
