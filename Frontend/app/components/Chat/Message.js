import React from 'react';
import PropTypes from 'prop-types';
import { parseJwt, getMemberRole, getSanctionType } from "../../globalJS";

export default class Message extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    const eventInfo = this.props.message.notificationType ? JSON.parse(this.props.message.notificationInfo) : null;
    return (
      this.props.message ?
        !this.props.message.notificationType ?
        <li className={`message ${parseJwt(localStorage.getItem('token')).UserId == this.props.message.senderId ? "right" : "left"}`}>
          {parseJwt(localStorage.getItem('token')).UserId != this.props.message.senderId
          && <span style={{opacity: 0.8}}>{this.props.message.senderName}</span>
          }
          <p style={{margin: '6px 0'}}>{this.props.message.text}</p>
          <div style={{textAlign: 'left', fontSize: 12, opacity: 0.5, marginBottom: 6}}>
            {(new Date(this.props.message.sentOn).getHours() < 10 ? '0' + new Date(this.props.message.sentOn).getHours() : new Date(this.props.message.sentOn).getHours()) + ':' + (new Date(this.props.message.sentOn).getMinutes() < 10 ? '0' + new Date(this.props.message.sentOn).getMinutes() : new Date(this.props.message.sentOn).getMinutes())}
          </div>
        </li>
          : <li className='message notify'>
              <span>
              {
                this.props.message.notificationType === 1 ?
                  `Преподаватель ${eventInfo.TeacherName} завершил курс`
                  : this.props.message.notificationType === 2 ?
                  `Учебный план группы принят`
                  : this.props.message.notificationType === 3 ?
                    `Пользователь ${eventInfo.DeclinedName} отклонил учебный план`
                    : this.props.message.notificationType === 4 ?
                      `В группе предложен учебный план`
                      : this.props.message.notificationType === 5 ?
                        `Группа сформирована`
                        : this.props.message.notificationType === 6 ?
                          `Пользователь ${eventInfo.InvitedName} принял ваше приглашение`
                          : this.props.message.notificationType === 7 ?
                            `Пользователь ${eventInfo.InvitedName} отклонил ваше приглашение`
                            : this.props.message.notificationType === 8 ?
                              `Пользователь ${eventInfo.InviterName} пригласил вас в группу "${eventInfo.GroupTitle}" на роль "${getMemberRole(eventInfo.SuggestedRole)}"`
                              : this.props.message.notificationType === 9 ?
                                `Пользователь ${eventInfo.Username} покинул группу`
                                : this.props.message.notificationType === 10 ?
                                  `Пользователь ${eventInfo.NewCreatorUsername} стал новым создателем группы`
                                  : this.props.message.notificationType === 11 ?
                                    `Пользователь ${eventInfo.Username} присоединился в группу`
                                    : this.props.message.notificationType === 12 ?
                                      `Пользователь ${eventInfo.SenderName} отправил репорт на пользователя ${eventInfo.SuspectedName} за нарушение правила "${eventInfo.BrokenRule}"`
                                      : this.props.message.notificationType === 13 ?
                                        `Пользователь ${eventInfo.ReviewerName} оставил отзыв о преподавателе группы`
                                        : this.props.message.notificationType === 14 ?
                                          `К вам была применена санкция "${getSanctionType(eventInfo.SanctionType)}" за нарушение правила "${eventInfo.BrokenRule}"`
                                          : this.props.message.notificationType === 15 ?
                                            `К пользователю ${eventInfo.Username} была применена санкция "${getSanctionType(eventInfo.SanctionType)}" за нарушение правила "${eventInfo.BrokenRule}"`
                                            : this.props.message.notificationType === 16 ?
                                              `Пользователь ${eventInfo.TeacherName} стал новым учителем группы`
                                              : this.props.message.notificationType === 17 ?
                                                `Примененная к вам санкция "${getSanctionType(eventInfo.SanctionType)}" за нарушение правила "${eventInfo.BrokenRule}" была отменена`
                                                : this.props.message.notificationType === 18 ?
                                                  `Примененная к пользователю ${eventInfo.Username} санкция "${getSanctionType(eventInfo.SanctionType)}" за нарушение правила "${eventInfo.BrokenRule}" была отменена`
                                                  : ''
              }
              </span>
          </li>
        :
        null
    )
  }
}

Message.propTypes = {
  user: PropTypes.string,
  username: PropTypes.string,
  content: PropTypes.string,
  time: PropTypes.string
};
