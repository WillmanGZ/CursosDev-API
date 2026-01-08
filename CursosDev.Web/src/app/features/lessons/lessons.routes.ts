import { Routes } from '@angular/router';
import { authGuard } from '../../core/auth';

export const LESSONS_ROUTES: Routes = [
  {
    path: '',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./lesson-list/lesson-list.component').then((m) => m.LessonListComponent),
  },
  {
    path: 'new',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./lesson-form/lesson-form.component').then((m) => m.LessonFormComponent),
  },
  {
    path: ':id/edit',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./lesson-form/lesson-form.component').then((m) => m.LessonFormComponent),
  },
];
