"use client";

import SubjectHandler from "../../../../components/Handler/SubjectHandler";

export default function DashboardSubjectUpdate({
  params,
}: {
  params: { id: number };
}) {
  const { id } = params;

  return <SubjectHandler id={id} />;
}
