/**
*
* NotifyCard
*
*/

import React from 'react';
import PropTypes from 'prop-types';
import {getMemberRole, getSanctionType} from "../../globalJS";
import {Card, Row, Col} from 'antd';
import config from "../../config";

class NotifyCard extends React.PureComponent { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props)
  }

  render() {
    const eventInfo = JSON.parse(this.props.notificationInfo);
    return (
      <Card
        hoverable
        className='notify-card'
        style={{width: '100%', cursor: 'default', position: 'relative', overflow: 'visible'}}
        bodyStyle={{padding: '14px 20px'}}
      >
        <div className='not-readed-btn'/>
        <Row>
          <Col span={16}>
            <span>
              {
                this.props.notificationType === 1 ?
                  `Преподаватель ${eventInfo.TeacherName} завершил курс в группе "${eventInfo.GroupTitle}"`
                : this.props.notificationType === 2 ?
                  `Учебный план группы "${eventInfo.GroupTitle}" принят`
                  : this.props.notificationType === 3 ?
                    `Пользователь ${eventInfo.DeclinedName} отклонил учебный план группы "${eventInfo.GroupTitle}"`
                    : this.props.notificationType === 4 ?
                      (
                        <div>
                          <span style={{display: 'block'}}>В группе {eventInfo.GroupTitle} предложен учебный план</span>
                          <a target='_blank' href={`${config.API_BASE_URL}/file/${eventInfo.CurriculumLink}`}>Скачать учебный план</a>
                        </div>
                      )
                      : this.props.notificationType === 5 ?
                        `Группа "${eventInfo.GroupTitle}" сформирована`
                        : this.props.notificationType === 6 ?
                          `Пользователь ${eventInfo.InvitedName} принял ваше приглашение в группу "${eventInfo.GroupTitle}"`
                          : this.props.notificationType === 7 ?
                            `Пользователь ${eventInfo.InvitedName} отклонил ваше приглашение в группу "${eventInfo.GroupTitle}"`
                            : this.props.notificationType === 8 ?
                              `Пользователь ${eventInfo.InviterName} пригласил вас в группу "${eventInfo.GroupTitle}" на роль "${getMemberRole(eventInfo.SuggestedRole)}"`
                              : this.props.notificationType === 9 ?
                                `Пользователь ${eventInfo.Username} покинул группу "${eventInfo.GroupTitle}"`
                                : this.props.notificationType === 10 ?
                                  `Пользователь ${eventInfo.NewCreatorUsername} стал новым создателем группы "${eventInfo.GroupTitle}"`
                                  : this.props.notificationType === 11 ?
                                    `Пользователь ${eventInfo.Username} присоединился в группу "${eventInfo.GroupTitle}"`
                                    : this.props.notificationType === 12 ?
                                      `Пользователь ${eventInfo.SenderName} отправил репорт на пользователя ${eventInfo.SuspectedName} за нарушение правила "${eventInfo.BrokenRule}"`
                                      : this.props.notificationType === 13 ?
                                        `Пользователь ${eventInfo.ReviewerName} оставил отзыв о преподавателе группы "${eventInfo.GroupTitle}"`
                                        : this.props.notificationType === 14 ?
                                          `К вам была применена санкция "${getSanctionType(eventInfo.SanctionType)}" за нарушение правила "${eventInfo.BrokenRule}"`
                                          : this.props.notificationType === 15 ?
                                            `К пользователю ${eventInfo.Username} была применена санкция "${getSanctionType(eventInfo.SanctionType)}" за нарушение правила "${eventInfo.BrokenRule}"`
                                            : this.props.notificationType === 16 ?
                                              `Пользователь ${eventInfo.TeacherName} стал новым учителем группы "${eventInfo.GroupTitle}"`
                                              : ''
              }
            </span>
          </Col>
          <Col span={8} style={{textAlign: 'right'}}>
            <span style={{fontSize: 14, opacity: 0.7}}>
              {`${new Date(this.props.occurredOn).getDate() < 10 ?
                '0' + new Date(this.props.occurredOn).getDate()
                  : new Date(this.props.occurredOn).getDate()}.${new Date(this.props.occurredOn).getMonth() < 10 ?
                    '0' + new Date(this.props.occurredOn).getMonth()
                     : new Date(this.props.occurredOn).getMonth()}.${new Date(this.props.occurredOn).getFullYear()}
                      ${new Date(this.props.occurredOn).getHours() < 10 ?
                        '0' + new Date(this.props.occurredOn).getHours()
                          : new Date(this.props.occurredOn).getHours()}:${new Date(this.props.occurredOn).getMinutes() < 10 ?
                            '0' + new Date(this.props.occurredOn).getMinutes()
                              : new Date(this.props.occurredOn).getMinutes()}`}
            </span>
          </Col>
        </Row>
        <Row>
          <span style={{wordWrap: 'break-word'}}>
            {this.props.text}
          </span>
        </Row>
      </Card>
    );
  }
}

NotifyCard.propTypes = {
  readed: PropTypes.bool,
  fromUser: PropTypes.string,
  date: PropTypes.string,
  text: PropTypes.string
};

export default NotifyCard;
