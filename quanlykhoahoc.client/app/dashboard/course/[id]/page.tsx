"use client";

import CourseHandler from "../../../../components/Handler/CourseHanlder";

export default function CourseCreate({ params }: { params: { id: number } }) {
  const { id } = params;

  return <CourseHandler id={id}/>
}
