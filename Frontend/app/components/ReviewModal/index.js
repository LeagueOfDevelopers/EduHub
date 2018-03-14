/**
*
* ReviewModal
*
*/

import React from 'react';
import { createStructuredSelector } from 'reselect';
import { connect } from 'react-redux';
import { addTeacherReview } from "../../containers/GroupPage/actions";
import { Form, Input, Col, Row, Modal, Button, message, Rate } from 'antd';
const FormItem = Form.Item;


class ReviewModal extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      title: '',
      review: '',
      stars: 0
    };

    this.onHandleReviewChange = this.onHandleReviewChange.bind(this);
    this.onHandleTitleChange = this.onHandleTitleChange.bind(this);
    this.onHandleRateChange = this.onHandleRateChange.bind(this);
  }

  onHandleTitleChange = (e) => {
    this.setState({title: e.target.value})
  };

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
        width='50%'
        closable={false}
        bodyStyle={{padding: '30px 10%'}}
        footer={null}
      >
        <Row style={{margin: '10px 0 44px'}}>
          <h2 style={{marginBottom: 0}}>{`Оставьте свой отзыв о преподавателе курса "${this.props.courseTitle}"`}</h2>
        </Row>
        <Form id='sign-in-form' onSubmit={this.handleSubmit}>
          <FormItem>
            <Input size='large' value={this.state.title} onChange={this.onHandleTitleChange} placeholder="Оглавление отзыва..."/>
          </FormItem>
          <FormItem>
            <Input.TextArea rows={6} value={this.state.review} onChange={this.onHandleReviewChange} placeholder="Здесь вы можете написать свой отзыв..."/>
          </FormItem>
          <FormItem
            label="Оцените преподавателя"
          >
            <Rate style={{ fontSize: 36 }} onChange={this.onHandleRateChange} value={this.state.stars} />
          </FormItem>
          <Row type='flex' justify='center' className='lg-center-container-item' style={{marginTop: 40}}>
            <Button
              className='group-btn lg-margin-right-0'
              type='primary'
              onClick={() => {
                this.props.addTeacherReview(this.props.groupId, this.state.title, this.state.review);
                this.props.handleCancel();
              }}
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

ReviewModal.propTypes = {

};

const mapStateToProps = createStructuredSelector({

});

function mapDispatchToProps(dispatch) {
  return {
    addTeacherReview: (groupId, title, text) => dispatch(addTeacherReview(groupId, title, text))
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(ReviewModal);
