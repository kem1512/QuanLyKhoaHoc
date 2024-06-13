"use client";

import SubjectDetailHandler from "../../../../components/Handler/SubjectDetailHandler";

export default function DashboardSubjectDetailUpdate({
  params,
}: {
  params: { id: number };
}) {
  const { id } = params;

  return <SubjectDetailHandler id={id} />;
}
