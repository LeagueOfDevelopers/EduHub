/**
*
* ReportModal
*
*/

import React from 'react';
import { createStructuredSelector } from 'reselect';
import { connect } from 'react-redux';
import { Form, Input, Col, Row, Modal, Button, message, Rate } from 'antd';
const FormItem = Form.Item;


class ReportModal extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {

    };
  }

  handleSubmit = (e) => {
    e.preventDefault();
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
        <Form id='sign-in-form' onSubmit={this.handleSubmit}>
          <FormItem
            label='Отправитель'
          >
            <div style={{lineHeight: '20px'}}>{this.props.report.senderName}</div>
          </FormItem>
          <FormItem
            label="Жалоба на"
          >
            <div style={{lineHeight: '20px'}}>{this.props.report.suspectedName}</div>
          </FormItem>
          <FormItem
            label="Причина"
          >
            <div style={{lineHeight: '20px'}}>{this.props.report.reason}</div>
          </FormItem>
          <FormItem
            label="Описание"
          >
            <div style={{lineHeight: '20px'}}>{this.props.report.description}</div>
          </FormItem>
          <Row type='flex' justify='center' className='lg-center-container-item' style={{marginTop: 40}}>
            <Button
              className='group-btn lg-margin-right-0'
              type='primary'
              onClick={() => this.props.onSanctionClick()}
              style={{width: 160, marginRight: 20, marginBottom: 10}}
            >
              Выписать санкцию
            </Button>
          </Row>
        </Form>
      </Modal>
    );
  }
}

ReportModal.propTypes = {

};

const mapStateToProps = createStructuredSelector({

});

function mapDispatchToProps(dispatch) {
  return {

  }
}

export default connect(mapStateToProps, mapDispatchToProps)(ReportModal);
