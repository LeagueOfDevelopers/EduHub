/**
 *
 * ReportModal
 *
 */

import React from 'react';
import { createStructuredSelector } from 'reselect';
import { connect } from 'react-redux';
import { annulSanction } from "../../containers/AdminPage/actions";
import { makeSelectCurrentSanction } from "../../containers/AdminPage/selectors";
import { Form, Input, Col, Row, Modal, Button, message, Rate } from 'antd';
const FormItem = Form.Item;


class SanctionModal extends React.Component { // eslint-disable-line react/prefer-stateless-function
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
            <div style={{lineHeight: '20px'}}>Первый модератор</div>
          </FormItem>
          <FormItem
            label="Санкция на"
          >
            <div style={{lineHeight: '20px'}}>Второй пользователь</div>
          </FormItem>
          <FormItem
            label="Тип санкции"
          >
            <div style={{lineHeight: '20px'}}>Запрет на преподавание</div>
          </FormItem>
          <FormItem
            label="Описание"
          >
            <div style={{lineHeight: '20px'}}>Более подробное описание репорта. Более подробное описание репорта. Более подробное описание репорта. Более подробное описание репорта.</div>
          </FormItem>
          <Row type='flex' justify='center' className='lg-center-container-item' style={{marginTop: 40}}>
            <Button className='group-btn' onClick={() => this.props.annulSanction(this.props.sanction.id)} style={{width: 160, marginBottom: 10}}>Отменить санкцию</Button>
          </Row>
        </Form>
      </Modal>
    );
  }
}

SanctionModal.propTypes = {

};

const mapStateToProps = createStructuredSelector({
  sanction: makeSelectCurrentSanction()
});

function mapDispatchToProps(dispatch) {
  return {
    annulSanction: (id) => dispatch(annulSanction(id))
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(SanctionModal);
