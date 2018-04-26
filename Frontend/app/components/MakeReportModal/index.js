/**
*
* MakeReportModal
*
*/

import React from 'react';
import { createStructuredSelector } from 'reselect';
import { connect } from 'react-redux';
import { makeReport } from "../../containers/ProfilePage/actions";
import { Form, Input, Col, Row, Modal, Button, message, Select, DatePicker } from 'antd';
const FormItem = Form.Item;


class MakeReportModal extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      reasonValue: '',
      descriptionValue: ''
    };

    this.onHandleReasonChange = this.onHandleReasonChange.bind(this);
    this.onHandleDescriptionChange = this.onHandleDescriptionChange.bind(this);
    this.applyReport = this.applyReport.bind(this);
  }

  onHandleReasonChange = (e) => {
    this.setState({reasonValue: e.target.value})
  };

  onHandleDescriptionChange = (e) => {
    this.setState({descriptionValue: e.target.value})
  };

  applyReport = () => {
     if(this.state.reasonValue && this.state.descriptionValue) {
       this.props.makeReport(this.props.userId, this.state.reasonValue, this.state.descriptionValue);
       this.props.handleCancel();
       this.setState({reasonValue: '', descriptionValue: ''});
     }
     else {
       message.error('Введите все данные!');
     }
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
        <Row style={{margin: '0px 0 40px'}}>
          <h2 style={{marginBottom: 0}}>Жалоба на {this.props.username}</h2>
        </Row>
        <Form>
          <FormItem>
            <Input placeholder='Причина репорта' onChange={this.onHandleReasonChange} value={this.state.reasonValue}/>
          </FormItem>
          <FormItem>
            <Input.TextArea placeholder='Описание нарушения' onChange={this.onHandleDescriptionChange} value={this.state.descriptionValue} autosize={{minRows: 3}}/>
          </FormItem>
          <Row type='flex' justify='center' className='lg-center-container-item' style={{marginTop: 42}}>
            <Button
              className='group-btn lg-margin-right-0'
              type='primary'
              style={{width: 160, marginRight: 20, marginBottom: 10}}
              onClick={this.applyReport}
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

MakeReportModal.propTypes = {

};

const mapStateToProps = createStructuredSelector({

});

function mapDispatchToProps(dispatch) {
  return {
    makeReport: (userId, reason, description) => dispatch(makeReport(userId, reason, description)),
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(MakeReportModal);
