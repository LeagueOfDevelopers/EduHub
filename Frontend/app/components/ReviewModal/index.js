/**
*
* ReviewModal
*
*/

import React from 'react';
import { Form, Input, Col, Row, Modal, Button, message, Rate } from 'antd';
const FormItem = Form.Item;


class ReviewModal extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      review: '',
      stars: 0
    };

    this.onHandleReviewChange = this.onHandleReviewChange.bind(this);
    this.onHandleRateChange = this.onHandleRateChange.bind(this);
  }

  onHandleReviewChange = (e) => {
    this.setState({review: e.target.value})
  };

  onHandleRateChange = (e) => {
    this.setState({stars: e})
  };

  handleSubmit = (e) => {
    e.preventDefault();
  };

  render() {
    return (
      <Modal
        visible={this.props.visible}
        onCancel={this.props.handleCancel}
        width='60%'
        closable={false}
        bodyStyle={{padding: '30px 10%'}}
        footer={null}
      >
        <Row style={{margin: '10px 0 54px'}}>
          <h2 style={{marginBottom: 0}}>{`Оставьте свой отзыв о курсе "${this.props.courseTitle}"`}</h2>
        </Row>
        <Form id='sign-in-form' onSubmit={this.handleSubmit}>
          <FormItem>
            <Input.TextArea rows={6} onChange={this.onHandleReviewChange} placeholder="Здесь вы можете написать свой отзыв..."/>
          </FormItem>
          <FormItem
            label="Оцените курс"
          >
            <Rate style={{ fontSize: 36 }} onChange={this.onHandleRateChange} value={this.state.stars} />
          </FormItem>
          <Row type='flex' justify='center' className='lg-center-container-item' style={{marginTop: 40}}>
            <Button className='group-btn lg-margin-right-0' type='primary' style={{width: 160, marginRight: 20, marginBottom: 10}}>Подтвердить</Button>
            <Button className='group-btn' onClick={this.props.handleCancel} style={{width: 160, marginBottom: 10}}>Отменить</Button>
          </Row>
        </Form>
      </Modal>
    );
  }
}

ReviewModal.propTypes = {

};

export default ReviewModal;
