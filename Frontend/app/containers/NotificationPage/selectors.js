import { createSelector } from 'reselect';

/**
 * Direct selector to the notificationPage state domain
 */
const selectNotificationPageDomain = (state) => state.get('notificationPage');

/**
 * Other specific selectors
 */


/**
 * Default selector used by NotificationPage
 */

const makeSelectNotificationPage = () => createSelector(
  selectNotificationPageDomain,
  (substate) => substate.toJS()
);

export default makeSelectNotificationPage;
export {
  selectNotificationPageDomain,
};
