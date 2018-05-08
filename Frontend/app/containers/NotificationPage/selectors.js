import { createSelector } from 'reselect';

/**
 * Direct selector to the notificationPage state domain
 */
const selectNotificationPageDomain = (state) => state.get('notificationPage');

const makeSelectNotifies = () => createSelector(
  selectNotificationPageDomain,
  (notifyPageState) => notifyPageState.get('notifies')
);

const makeSelectInvites = () => createSelector(
  selectNotificationPageDomain,
  (notifyPageState) => notifyPageState.get('invites')
);

const makeSelectNeedUpdate = () => createSelector(
  selectNotificationPageDomain,
  (notifyPageState) => notifyPageState.get('needUpdate')
);

const makeSelectNotifiesSettings = () => createSelector(
  selectNotificationPageDomain,
  (notifyPageState) => notifyPageState.get('notifiesSettings')
);

export {
  selectNotificationPageDomain,
  makeSelectNotifies,
  makeSelectInvites,
  makeSelectNeedUpdate,
  makeSelectNotifiesSettings
};
