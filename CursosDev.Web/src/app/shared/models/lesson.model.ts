export interface Lesson {
  id: string;
  courseId: string;
  title: string;
  order: number;
  isDeleted: boolean;
  createdAt: string;
  updatedAt: string;
}

export interface CreateLessonRequest {
  courseId: string;
  title: string;
  order: number;
}

export interface UpdateLessonRequest {
  id: string;
  title: string;
  order: number;
}
